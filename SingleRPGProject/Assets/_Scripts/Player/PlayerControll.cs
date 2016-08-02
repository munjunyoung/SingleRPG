using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


public class PlayerControll : MonoBehaviour {

    

    //플레이어 데이터 저장할떄 필요한변수들   레벨, 아이템, 경험치, 돈, 스킬배운것들, 스킬창유지 포션 개수
    public float PlayerLevel;
    public float PlayerCurrentExPoint;
    public float playerGold;
    GameObject inven;
    GameObject Skill;
    GameObject SkillWindow;
    //SaveData SaveData;
    public List<int> itemid = new List<int>();
    public int itemNumber;
    public List<bool> wSkillOn = new List<bool>();
    public List<int> Skillid = new List<int>();
    public int potionNumber;

    //스탯
    public float powerStat;
    public float healthStat;
    public float SpeedStat;

    public float attackPower;//힘스탯*5 =공격력 기본공격력
    public float Health;
    public float AttackSpeed;
    public float statPoint;

    GameObject abilityObject;




    public float speed = 3f;
    public float turnSpeed = 10f;
    Vector3 movement;
    public GameObject cam;//캠
    Rigidbody playerRigidbody;

    public Animator anim;

    //방향키 더블클릭시 달리기할때 필요한 변수들
    bool oneClick;
    float upDoubleClickTimer = 0.0f;
    float downDoubleClickTimer = 0.0f;
    float leftDoubleClickTimer = 0.0f;
    float rightDoubleClickTimer = 0.0f;
    float TimeLeft = 0.5f;
    int buttonTapNumber = 0;
    // Use this for initialization

    GameObject popupParent;
    GameObject popupPrefab;
    GameObject popup;

   

   

    //애니메이션 셋팅
    bool walking;
    bool running;
    bool attacking;

    bool jumping;
    bool idle;
    bool battleOn;

    int tap;

    public List<GameObject> skillsEffectPrefab = new List<GameObject>();
    public List<GameObject> skillsEffect = new List<GameObject>(); //리소스에서 받아올 리스트

    GameObject SlashEffectPrefab1;
    GameObject SlashEffect1;
    //그냥스킬로 쳐리해서쓰진않는다..
    GameObject HealingEffectPrefab;
    GameObject HealingEffect;
   
    GameObject LevelUpEffectPrefab;
    GameObject LevelUpEffect;
    
    GameObject skill6HandEffectPrefab;
    GameObject skill6HandEffect;

    GameObject skill5BodyEffectPrefab;
    GameObject skill5Bodyffect;

    public List<bool> skills = new List<bool>();
    

    private Animator avatar;


    public bool isDead;

    public bool TriggerAttack;//공격애니메이션에 임팩트 순간에 공격할수 있도록
    bool stopMove; //공격애니메이션에 이동을 정지 시킬떄
   

    //상태마다 무기위치를 지정하기위해서 선언한 오브젝트들 (idle일때위치와 공격등일때등등)
    GameObject Weapon;
    GameObject WeaponPosition;
    GameObject WeaponPositionIdle;



    string PlayerID;
    //플레이어 UI 설정들 
    public float playerHealth;
    public float currentHealth;

    float playerMp;
    public float currentMp;
    
    float playerMaxExpPoint=100;
    float playerPerExpPoint;

    SkillWindowControlScript playerSkillOpen;

    Text PlayerLevelText;
    Text PlayerIdText;
    
    GameObject ExpSlider;
    Text ExpText;

    GameObject MpSlider;
    Text MpText;

    GameObject healthSlider;
    public Image fill;
    Image damageImage;
    Text lifeText;
    float flashSpeed = 1f;//번쩍거리는스피드?
    Color flashColor = new Color(1f, 0f, 0f, 0.1f);
    Color MaxHealthColor = Color.green; // ui 체력바 색깔
    Color CenterHealthColor = Color.green;
    Color MinHealthColor = Color.red;
  
    
    
    //배틀타이머 선언
    float battleTimer = 0f;

    Transform Enemy; //공격당하고나서 바로 바라보기 위함



    //sendMessage사용..
    GameObject ExitPanel;



    public bool _grappedfromEnemy;




    private GameObject AlarmPanel;
    private float alarmTimer;




    void Awake () {

       
        avatar = GameObject.Find("Player").GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody>();
        
        anim = GetComponent<Animator>();
        oneClick = true;
        Weapon = GameObject.Find("Weapon");
        WeaponPosition = GameObject.Find("WeaponEquipObject");//공격할경우 무기를 손에장착
        WeaponPositionIdle = GameObject.Find("WeaponIdleObject");//공격하지 않을경우 무기를 등에 장착
         damageImage = GameObject.Find("DamageImage").GetComponent<Image>(); // 맞을떄 플래시 이미지
        inven = GameObject.Find("InventoryObject");

        // Enemy = GameObject.Find("Enemy").GetComponent<Transform>();
   //     DontDestroyOnLoad(this.gameObject);
   //     DontDestroyOnLoad(cam.gameObject);





        //UI설정

        healthSlider = GameObject.Find("HealthSlider"); // 체력바
        MpSlider = GameObject.Find("MpSlider");
        ExpSlider = GameObject.Find("ExpSlider");

        PlayerIdText = GameObject.Find("NameText").GetComponent<Text>();
        PlayerLevelText = GameObject.Find("LevelText").GetComponent<Text>();
        lifeText = GameObject.Find("LifeText").GetComponent<Text>();
        MpText = GameObject.Find("MPText").GetComponent<Text>();
        ExpText = GameObject.Find("ExpText").GetComponent<Text>();
        playerSkillOpen = GameObject.Find("SkillObject").GetComponent<SkillWindowControlScript>();

        SkillWindow = GameObject.Find("SkillObject");
        //스킬창 레벨에따른 스킬창open설정

        AlarmPanel = GameObject.Find("AlarmPanel");




        //스킬설정
        for (int i = 0; i < 7; i++)
        {
            skills.Add(false);
            skillsEffect.Add(null);
            skillsEffectPrefab.Add(null);
        }

        //스킬이펙트오브젝트가져오기
        skillsEffectPrefab[0] = Resources.Load<GameObject>("SkillEffect/Skill" + "1" + "Effect") as GameObject;
        skillsEffectPrefab[1] = Resources.Load<GameObject>("SkillEffect/Skill" + "2" + "Effect") as GameObject;
        skillsEffectPrefab[2] = Resources.Load<GameObject>("SkillEffect/Skill" + "3" + "Effect") as GameObject;
        skillsEffectPrefab[3] = Resources.Load<GameObject>("SkillEffect/Skill" + "4" + "Effect") as GameObject;
        skillsEffectPrefab[4] = Resources.Load<GameObject>("SkillEffect/Skill" + "5" + "Effect") as GameObject;
        skillsEffectPrefab[5] = Resources.Load<GameObject>("SkillEffect/Skill" + "6" + "Effect") as GameObject;
        skillsEffectPrefab[6] = Resources.Load<GameObject>("SkillEffect/Skill" + "7" + "Effect") as GameObject;



        LevelUpEffectPrefab = Resources.Load<GameObject>("SkillEffect/LevelUpEffect") as GameObject;
        skill5BodyEffectPrefab =Resources.Load<GameObject>("SkillEffect/Skill5EffectBody") as GameObject;
        skill6HandEffectPrefab=Resources.Load<GameObject>("SkillEffect/Skill6EffectHand") as GameObject;
        SlashEffectPrefab1 = Resources.Load<GameObject>("SkillEffect/AttackSlashEffect1");
        HealingEffectPrefab = Resources.Load<GameObject>("SkillEffect/HealingEffect");


        popupParent = GameObject.Find("PopUpObject");
        popupPrefab = Resources.Load<GameObject>("SkillEffect/PopUpEffect");
        ExitPanel = GameObject.Find("ExitPanel");
        abilityObject = GameObject.Find("CharacterAbilityObject");
      


        //SaveData = GameObject.Find("GameControllerObject").GetComponent<SaveData>();
        for (int i = 0; i < 7; i++)
        {
            wSkillOn.Add(false);
        }



    }
    void Start()
    {
        ///데이터 로드 
        

        DataLoadSetting();//데이터 로드 새로시작등
        

        inven.GetComponent<InventoryScript>().Addgold((int)playerGold);
        UpdateExpSilderUI(0);


        Weapon.transform.SetParent(WeaponPositionIdle.transform);//아닐경우 등에장착
        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);


        PlayerID = "MOOOOOON";
        PlayerIdText.text = PlayerID;

        healthSetting();
        


        playerMp = 100;
        currentMp = playerMp;
        MpSlider.GetComponent<Slider>().maxValue = (int)playerMp;
        MpSlider.GetComponent<Slider>().value = (int)currentMp;


     
        playerPerExpPoint = (PlayerCurrentExPoint / playerPerExpPoint);

        ExpSlider.GetComponent<Slider>().maxValue = (int)playerMaxExpPoint;
        ExpSlider.GetComponent<Slider>().value = (int)PlayerCurrentExPoint;

        PlayerLevelText.text = "" + PlayerLevel;

        playerPerExpPoint = (int)((PlayerCurrentExPoint / playerMaxExpPoint) * 100);
        ExpText.text = "" + playerPerExpPoint + "%" + "    " + PlayerCurrentExPoint + " / " + playerMaxExpPoint;





        abilityObject.GetComponent<abilityScript>().SettingCharacterPanel();
        AlarmPanel.SetActive(false);

    }

    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                attacking = true;
                speed = 3.0f; //공격후에 스피드 다시 원상복귀만약에 달리고있었을경우
                battleOn = true;
                battleTimer = 0.0f; //배틀타이머 시작 
            }
            else if (!Input.GetKey(KeyCode.LeftControl))
            {
                attacking = false;
            }



            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (avatar.GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.unarmed_jump"))
                {
                    jumping = true;
                }
            }
            else if (!Input.GetKeyUp(KeyCode.Space))
            {


            }

            if (AlarmPanel.activeSelf)
            {
                alarmTimer += Time.deltaTime;
            }

            if (alarmTimer > 3)
            {
                if (AlarmPanel.activeSelf)
                {
                    AlarmPanel.SetActive(false);
                }
            }
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {

        if (!isDead)
        {
            //공격
            
            SkillSetting();//스킬 실행함수

            BattleSetting();//idle 배틀시작 

            //캐릭터 이동

            float h = Input.GetAxisRaw("Horizontal");
            float v = Input.GetAxisRaw("Vertical");
            bool moving = h != 0f || v != 0f;

            if (moving == true && !stopMove&& attacking == false) //stopMove는 애니메이션 자체에서 추가 
            {
               
                Move(h, v, speed); // 이동 + 플레이어 turing 함수
               
                    Run();// 방향키 연속누를시 달리는 함수
                
                attacking = false;
            }
            if (jumping == true)
            {
                Jump();
                
            }

            if (_grappedfromEnemy == false)
            {
                Turning(h, v); //스킬이나 공격할때 움직이진 못해도 돌릴수는있도록
            }
            //  Debug.Log(skill1On);


            //애니매이팅
            anim.SetBool("Skill1On", skills[0]);
            anim.SetBool("Skill2On", skills[1]);
            anim.SetBool("Skill3On", skills[2]);
            anim.SetBool("Skill4On", skills[3]);
            anim.SetBool("Skill5On", skills[4]);
            anim.SetBool("Skill6On", skills[5]);
            anim.SetBool("Skill7On", skills[6]);
            
            anim.SetBool("AttackOn", attacking);
            anim.SetBool("JumpOn", jumping);
            anim.SetBool("IsWalking", moving && walking);
            anim.SetBool("IsRun", moving && running);
            anim.SetBool("IsIdleBattle", battleOn);

        }



        //체력바 UI 업데이트 함수  //죽든 안죽든 상관없어야함
        
        UpdatLifeSliderUI();
        UpdatMpSliderUI();



    }

    public void DataLoadSetting()
    {
        if (SaveControll.setting == false)
        {
            statPoint = 1;
            powerStat = 1;
            healthStat = 1;
            SpeedStat = 1;
            

            AttackSpeed = 1.5f + (SpeedStat * 0.1f);
            attackPower = powerStat * 5;
            Health = healthStat * 10;

           

            PlayerLevel = 1;
            PlayerCurrentExPoint = 0;
            playerMaxExpPoint = PlayerLevel*50;
            playerGold = 100;
            itemNumber = 1;
            itemid.Add(3);
            inven.GetComponent<InventoryScript>().AddItem(itemid[0]);
        }
        else if (SaveControll.setting == true)// 로드됫을때
        {
            statPoint = SaveControll.pData.statpoint;
            powerStat = SaveControll.pData.powerStat;
            healthStat = SaveControll.pData.healthstat;
            SpeedStat = SaveControll.pData.speedStat;

            //데이터 셋팅
            AttackSpeed = 1.5f + (SpeedStat * 0.1f);
            attackPower = powerStat * 5;
            Health = healthStat * 10;
            anim.SetFloat("attackSpeed", AttackSpeed);

            PlayerLevel = SaveControll.pData.level;
            PlayerCurrentExPoint = SaveControll.pData.exp;
            playerGold = SaveControll.pData.gold;
            itemNumber = SaveControll.pData.itemNumber;

            for (int i = 0; i < 50; i++)
            {
                itemid.Add(SaveControll.pData.itemID[i]);
            }

            for (int j = 0; j < 7; j++)
            {
                wSkillOn[j] = SaveControll.pData.SkillOpen[j];
            }
            playerMaxExpPoint = (PlayerLevel * 50);

            for (int i = 0; i < 50; i++)
            {

                if (itemid[i] != -1)
                {
                    if (itemid[i] == 10)//포션이라면
                    {
                        for (int j = 0; j < SaveControll.pData.potionNumber; j++)
                        {
                            inven.GetComponent<InventoryScript>().AddItem(itemid[i]); //포션이 있으면 포션갯수만큼 추가해준당
                        }
                    }
                    else //포션이 아니라면
                    {
                        inven.GetComponent<InventoryScript>().AddItem(itemid[i]);
                    }
                }


            }
        }

    }

    //플레이어 체력관리
    public void healthSetting()
    {
        playerHealth = (50 * (PlayerLevel + 1)) + Health;
        currentHealth = playerHealth;
        healthSlider.GetComponent<Slider>().maxValue = (int)playerHealth;
        healthSlider.GetComponent<Slider>().value = (int)currentHealth;
    }

    /// <summary>
    /// 포션먹기
    /// </summary>
    public void EatFood(int potionAmount)
    {
       
            currentHealth += potionAmount;//피채우기
            if (playerHealth < currentHealth) //피가 최대체력보다 높을떄는 그냥 currenthealth로
            {
                currentHealth = playerHealth;
            }
            healthSlider.GetComponent<Slider>().value = (int)currentHealth;
        if(HealingEffect!= null)
        {
            HealingEffect = null;
        }

        HealingEffect = Instantiate(HealingEffectPrefab);
        HealingEffect.transform.SetParent(this.transform);
        HealingEffect.transform.localScale = Vector3.one;
        HealingEffect.transform.localPosition = new Vector3(0, 0f, 0f);
        HealingEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);

        popup = Instantiate(popupPrefab);
        popup.transform.SetParent(popupParent.transform);
        popup.transform.localPosition = this.transform.position;
        popup.transform.localScale = Vector3.one;

        popup.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z));
        popup.GetComponent<FloatingText>().SetText("<color=#33cc33><b>\n +" + potionAmount.ToString() + "\n</b></color>");




    }






    public void UpdateExpSilderUI(int exp)
    {
       
        PlayerCurrentExPoint += exp;

        if(PlayerCurrentExPoint>=playerMaxExpPoint)//경험치가 꽉찼을떄 (레벨업할때)!!!!!!!!
        {
            PlayerLevel++;//플레이어 레벨증가
            statPoint++;//스탯포인트 증가
            abilityObject.GetComponent<abilityScript>().UpdateAbilityPoint();

            PlayerLevelText.text = ""+PlayerLevel;
            PlayerCurrentExPoint = PlayerCurrentExPoint - playerMaxExpPoint;
            ExpSlider.GetComponent<Slider>().value = PlayerCurrentExPoint;
            playerMaxExpPoint = (PlayerLevel * 50);//경험치를 레벨이 오를때마다 요구량 증가
            ExpSlider.GetComponent<Slider>().maxValue = playerMaxExpPoint;
            playerPerExpPoint = (int)((PlayerCurrentExPoint / playerMaxExpPoint) * 100);
            ExpText.text = "" + playerPerExpPoint + "%" + "    " + PlayerCurrentExPoint + " / " + playerMaxExpPoint;


            //레벨업하면 체력 풀로채워짐
            healthSetting();
            

            ////이펙트 생성..
            if (LevelUpEffect != null) //만약 켜져있는중에 또실행하면?
            {
                LevelUpEffect = null;
            }
            else if (LevelUpEffect == null)
            {
                LevelUpEffect = Instantiate(LevelUpEffectPrefab);
                LevelUpEffect.transform.SetParent(this.transform);
                LevelUpEffect.transform.localScale = Vector3.one;
                LevelUpEffect.transform.localPosition = new Vector3(0, 0f, 0f);
                LevelUpEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
            
            playerSkillOpen.RockOFFSkill((int)PlayerLevel);
            if (PlayerLevel < 8)
            {
                alarmText(PlayerLevel +"번째 스킬이 열렸습니다. K 를 눌러서 확인하세요");
            }

        }
        else
        {
            ExpSlider.GetComponent<Slider>().value = PlayerCurrentExPoint;
            ExpSlider.GetComponent<Slider>().maxValue = playerMaxExpPoint;

            playerPerExpPoint = (int)((PlayerCurrentExPoint / playerMaxExpPoint) * 100);
            ExpText.text = "" + playerPerExpPoint + "%"+"    "+ PlayerCurrentExPoint+" / "+ playerMaxExpPoint;
        }

    }


    void UpdatLifeSliderUI()
    {
        
        //텍스트 설정및 슬라이더 PLAYERHEALTH 맥스값설정 /////////////////체력 슬라이더 업데이트
        if (currentHealth >= 0)
        {
            lifeText.text = (int)currentHealth + "/" + (int)playerHealth; //life 설정 
        }
        else
        {
            lifeText.text = "0/" + playerHealth; //life 설정 
        }


        if(currentHealth < playerHealth&&isDead==false)
        {
           
            if(currentHealth > playerHealth)
            {
                currentHealth = playerHealth;
            }
            else if(currentHealth<=0)
            {
                currentHealth = 0;
            }
            else
            {                    currentHealth = currentHealth + (0.5f * Time.deltaTime);
                    healthSlider.GetComponent<Slider>().value = currentHealth;
              
            }

        }
        

        //피가 떨어질떄마다 ui 컬러 변경
        if (currentHealth > playerHealth*(0.7))
        {
            fill.color = MaxHealthColor;
        }
        if (currentHealth > playerHealth*0.3 && currentHealth <= playerHealth * (0.7)) //색깔변경 추가할것
        {
            fill.color = Color.Lerp(CenterHealthColor, MaxHealthColor, flashSpeed * Time.deltaTime);
        }
        else if (currentHealth >= 0 && currentHealth <= playerHealth * 0.3)
        {
            fill.color = Color.Lerp(MinHealthColor, CenterHealthColor, flashSpeed * Time.deltaTime);
        }

        //색상 다시 돌아오게 만듦
        damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
    }
    

    void UpdatMpSliderUI()
    {
        //텍스트 설정및 슬라이더 PLAYERHEALTH 맥스값설정  ///////////////Mp 업데이트
        if (currentMp >= 0)
        {
            MpText.text = (int)currentMp + "/" + (int)playerMp; //life 설정 
        }
        else
        {
            MpText.text = "0/" + playerMp; //life 설정 
        }


        if (currentMp < playerMp && isDead == false)
        {

            if (currentMp > playerMp)
            {
                currentMp = playerMp;
            }
            else if (currentMp < 0)
            {
                currentMp = 0;
            }
            else
            {
                currentMp = currentMp + (10f * Time.deltaTime);
                MpSlider.GetComponent<Slider>().value = currentMp;
            }

        }

    }



    void SkillSetting()
    {
        for (int i = 0; i < 7; i++)
        {
            if (skills[i] == true)
            {
                Weapon.transform.SetParent(WeaponPosition.transform);//배틀온일때는 무기를 손에장착
                Weapon.transform.localPosition = Vector3.zero;
                Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);

                if (avatar.GetCurrentAnimatorStateInfo(0).fullPathHash == Animator.StringToHash("Base Layer.Skill" + (i + 1))) //스킬이 씹혀서 안나갈때가있음
                {
                    

                    if (skills[0] == true) //파이어볼날리기?
                    {
                        if (skillsEffect[0] != null) //만약 켜져있는중에 또실행하면?
                        {

                            Destroy(skillsEffect[0].gameObject);
                            skillsEffect[0] = null;
                        }
                        skillsEffect[0] = Instantiate(skillsEffectPrefab[0]);
                        skillsEffect[0].transform.SetParent(this.transform);
                        skillsEffect[0].transform.localScale = Vector3.one;
                        skillsEffect[0].transform.localPosition = Vector3.zero;
                        skillsEffect[0].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        skills[i] = false;
                    }
                    else if (skills[1] == true) // 후반에 터뜨리기
                    {
                        if (skillsEffect[1] != null) //만약 켜져있는중에 또실행하면?
                        {

                            Destroy(skillsEffect[1].gameObject);
                            skillsEffect[1] = null;
                        }
                        skillsEffect[1] = Instantiate(skillsEffectPrefab[1]);
                        skillsEffect[1].transform.SetParent(this.transform);
                        skillsEffect[1].transform.localScale = Vector3.one;
                        skillsEffect[1].transform.localPosition = Vector3.zero;
                        skillsEffect[1].transform.localRotation = Quaternion.Euler(0, 0, 0);
                        skills[i] = false;
                    }
                    else if (skills[2] == true) //독뎀 차징 
                    {

                        GameObject weaponTransfom = GameObject.Find("Weapon");
                        if (skillsEffect[2] != null) //만약 켜져있는중에 또실행하면?
                        {

                            Destroy(skillsEffect[2].gameObject);
                            skillsEffect[2] = null;
                        }
                        skillsEffect[2] = Instantiate(skillsEffectPrefab[2]);
                        skillsEffect[2].transform.SetParent(weaponTransfom.transform);
                        skillsEffect[2].transform.localScale = Vector3.one;
                        skillsEffect[2].transform.localPosition = Vector3.zero;
                        skillsEffect[2].transform.localRotation = Quaternion.Euler(-90, 0, 0);
                        skills[i] = false;

                    }
                    else if (skills[3] == true) // 플레이어 체력증가?방어력증가
                    {
                        if (skillsEffect[3] != null) //만약 켜져있는중에 또실행하면?
                        {

                            Destroy(skillsEffect[3].gameObject);
                            skillsEffect[3] = null;
                        }
                        skillsEffect[3] = Instantiate(skillsEffectPrefab[3]);
                        skillsEffect[3].transform.SetParent(this.transform);
                        skillsEffect[3].transform.localScale = Vector3.one;
                        skillsEffect[3].transform.localPosition = Vector3.zero;
                        skillsEffect[3].transform.localRotation = Quaternion.Euler(0, 0, 0);


                    }
                    else if (skills[4] == true) //점프공격이니 신경쓰지말것
                    {
                        GameObject weaponTransfom = GameObject.Find("Weapon");
                        if (skill5Bodyffect != null)
                        {
                            skill5Bodyffect = null;
                        }

                        if (skill5Bodyffect == null)
                        {
                            skill5Bodyffect = Instantiate(skill5BodyEffectPrefab);
                            skill5Bodyffect.transform.SetParent(weaponTransfom.transform);
                            skill5Bodyffect.transform.localScale = Vector3.one;
                            skill5Bodyffect.transform.localPosition = Vector3.zero;
                            skill5Bodyffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }

                    }
                    else if (skills[5] == true) //중력장
                    {
                        GameObject weaponTransfom = GameObject.Find("Weapon");
                        if (skill6HandEffect != null)
                        {
                            skill6HandEffect = null;
                        }

                        if (skill6HandEffect == null)
                        {
                            skill6HandEffect = Instantiate(skill6HandEffectPrefab);
                            skill6HandEffect.transform.SetParent(weaponTransfom.transform);
                            skill6HandEffect.transform.localScale = Vector3.one;
                            skill6HandEffect.transform.localPosition = Vector3.zero;
                            skill6HandEffect.transform.localRotation = Quaternion.Euler(0, 0, 0);
                        }
                    }

                    else if (skills[6] == true) //휠윈드
                    {
                        if (skillsEffect[6] != null) //만약 켜져있는중에 또실행하면?
                        {

                            Destroy(skillsEffect[6].gameObject);
                            skillsEffect[6] = null;
                        }
                        skillsEffect[6] = Instantiate(skillsEffectPrefab[6]);
                        skillsEffect[6].transform.SetParent(this.transform);
                        skillsEffect[6].transform.localScale = Vector3.one;
                        skillsEffect[6].transform.localPosition = Vector3.zero;
                        skillsEffect[6].transform.localRotation = Quaternion.Euler(0, 0, 0);

                    }


                    skills[i] = false;
                }
                speed = 3.0f;

                battleTimer = 0.0f;

            }
        }

    }


    void BattleSetting() // battle on/off 일때 각각 애니메이션 셋팅과 무기 위치
    {

        if (!isDead)
        {
            if (battleOn == true)
            {
                battleTimer += Time.deltaTime;
                Weapon.transform.SetParent(WeaponPosition.transform);//배틀온일때는 무기를 손에장착
                Weapon.transform.localPosition = Vector3.zero;
                Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);

            }
            else
            {
               
            }
            
            if (battleTimer >= 5.0f)
            {
                battleTimer = 0.0f;
                battleOn = false;
            }
        }
        else // 죽으면
        {

        }
    }


    
    void Run()
    { 
        if (Input.GetKeyDown(KeyCode.UpArrow)||Input.GetKeyDown(KeyCode.W))
        {
            if (oneClick == true)//원클릭일때
            {
                if (Time.time - upDoubleClickTimer < TimeLeft && upDoubleClickTimer != 0)//현재타임과 저장한 타임의 차값이 설정한 delay값보다 작을시
                {
                  
                    oneClick = false;
                }
            }

            if (oneClick == true)//한번클릭일때
            {
                speed = 3.0f;
                upDoubleClickTimer = Time.time;
            }
            else if (oneClick == false)//더블클릭일떄
            {
                //다시 리셋시켜줌
                speed = 5.5f;
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)|| Input.GetKeyDown(KeyCode.S))
        {
            if (oneClick == true)//원클릭일때
            {
                if (Time.time - downDoubleClickTimer < TimeLeft && downDoubleClickTimer != 0)//현재타임과 저장한 타임의 차값이 설정한 delay값보다 작을시
                {
                  
                    oneClick = false;
                }
            }


            if (oneClick == true)//한번클릭일때
            {
              
                speed = 3.0f;
               
                downDoubleClickTimer = Time.time;
            }
            else if (oneClick == false)//더블클릭일떄
            {
                speed = 5.5f;
              

            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)|| Input.GetKeyDown(KeyCode.A))
        {
            if (oneClick == true)//원클릭일때
            {
                if (Time.time - leftDoubleClickTimer < TimeLeft && leftDoubleClickTimer != 0)//현재타임과 저장한 타임의 차값이 설정한 delay값보다 작을시
                {
                    oneClick = false;
                }
            }


            if (oneClick == true)//한번클릭일때
            {
                speed = 3.0f;
                leftDoubleClickTimer = Time.time;
            }
            else if (oneClick == false)//더블클릭일떄
            {
                //다시 리셋시켜줌
                speed = 5.5f;

            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)|| Input.GetKeyDown(KeyCode.D))
        {
            if (oneClick == true)//원클릭일때
            {
                if (Time.time - rightDoubleClickTimer < TimeLeft && rightDoubleClickTimer != 0)//현재타임과 저장한 타임의 차값이 설정한 delay값보다 작을시
                {
                    oneClick = false;
                }
            }


            if (oneClick == true)//한번클릭일때
            {
                speed = 3.0f;
                rightDoubleClickTimer = Time.time;
            }
            else if (oneClick == false)//더블클릭일떄
            {
                //다시 리셋시켜줌
                speed = 5.5f;

            }
        }


        if (speed == 3.0f)
        {
            walking = true;
            running = false;
        }
        else if (speed == 5.5f)
        {
            walking = false;
            running = true;
        }

    }



    void Jump()
    {
        if(!jumping)
        {
            return;
        }
        else if (avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.unarmed_jump")||
            avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.unarmed_jump_running"))
        {
            //playerRigidbody.AddForce(Vector3.up * 100);
           // StartCoroutine(addJump(0.3f));
            jumping = false;
            stopMove = false; // 무빙방지
            TriggerAttack = false;//공격하다 점프하면 온되있는걸방지
        }



    }


    //무빙함수
    void Move(float h, float v, float speed)
    {

        // 카메라의 포지션 이동
        // newPosition = target.position;

        //transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        //카메라 자유시점 적용
        //무빙
        Quaternion newRotation = Quaternion.Euler(0, cam.GetComponent<CameraFollow>().currentAngleX, 0);

        movement.Set(h, 0f, v);
        Vector3 newPosition = newRotation * movement;

        newPosition = newPosition.normalized * speed * Time.deltaTime;
        //movement = new Vector3(h, 0.0f,v);

        playerRigidbody.MovePosition(transform.position + newPosition);
        // playerRigdbody.AddForce(movement * speed);

        // 방향키를 누르는 움직임의 방향에따라 캐릭터 rotation turning
     /*   Quaternion Rotation = Quaternion.Euler(0, cam.currentAngleX, 0);
        Vector3 _Direction = new Vector3(h, 0.0f, v);
        Vector3 newDirection = Rotation * _Direction;
        Quaternion _Rotation = Quaternion.LookRotation(newDirection);
        Quaternion newRotationTurning = Quaternion.Lerp(playerRigidbody.rotation, _Rotation, turnSpeed * Time.deltaTime);//시작값과 나중값을 넣은후에 
        transform.rotation = newRotationTurning;*/

    }


    void Turning(float h, float v)
    {
        if (h != 0 || v != 0)
        {
            Quaternion Rotation = Quaternion.Euler(0, cam.GetComponent<CameraFollow>().currentAngleX, 0);
            Vector3 _Direction = new Vector3(h, 0.0f, v);
            Vector3 newDirection = Rotation * _Direction;
            Quaternion _Rotation = Quaternion.LookRotation(newDirection);
            Quaternion newRotationTurning = Quaternion.Lerp(playerRigidbody.rotation, _Rotation, turnSpeed * Time.deltaTime);//시작값과 나중값을 넣은후에 
            transform.rotation = newRotationTurning;
        }
    }



    public void TakeDamage(int damageAmount) //공격을 받을때
    {
        if (isDead)
        {
            return;
        }

        battleOn = true;//배틀 시작 변수 트루전환
        battleTimer = 0.0f;//맞을때마다 타이머를 리셋시켜줌
        currentHealth -= damageAmount;//피깎
        healthSlider.GetComponent<Slider>().value = currentHealth;//슬라이더값과 최근 플레이어 피값 넣기
        damageImage.color = flashColor;//타격당할때 플래시 이미지 색상 빨간색으로 변경
        TriggerAttack = false; //공격하다 중지되어 트리거온이되는걸 막기위함
        anim.SetTrigger("takeDamage");


        popup = Instantiate(popupPrefab);
        popup.transform.SetParent(popupParent.transform);
        popup.transform.localPosition = this.transform.position;
        popup.transform.localScale = Vector3.one;

        popup.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z));
        popup.GetComponent<FloatingText>().SetText("<color=#ffff66><b>\n -" + damageAmount.ToString()+ "\n</b></color>");
      


        /*   if (Enemy != null)
           {
               transform.LookAt(Enemy);//때린적을 바라보기
           }*/


        if (currentHealth <= 0)
        {
             Death();
        }


    }

    void Death() //죽음
    {
        isDead = true;
        anim.SetTrigger("Death");
        StartCoroutine(DeathNext(5));
      
    }



    //코루틴

    IEnumerator Skillfalse(float second, int check) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);
       
        skills[check] = false;
    }

    IEnumerator addJump(float second) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);

        playerRigidbody.AddForce(Vector3.up * 250);//, ForceMode.Force);

    }
    

    IEnumerator DeathNext(float second)
    {
        yield return new WaitForSeconds(second);
        Application.LoadLevel("Level 01");
        isDead = false;
        anim.SetBool("Live", !isDead);
        this.transform.position = Vector3.zero;
        this.transform.rotation = Quaternion.Euler(Vector3.zero);
       
        currentHealth = playerHealth;
        healthSlider.GetComponent<Slider>().value = (int)currentHealth;

        StartCoroutine(liveset(2));
       
    }
    IEnumerator liveset(float second)
    {
        yield return new WaitForSeconds(second);
        anim.SetBool("Live", false);
    }


    /// /////////////////////////////
    /// </summary>
    //애니메이션에 쳐넣기 위한 함수들
    public void runningstop()
    {
        oneClick = true;
    }
    public void moveStart()
    {
        
        stopMove = false;
    }
    public void moveStop()
    {
        stopMove = true;
    }
    public void BattleStart()
    {
        battleOn = true;
    }

    public void TriggerAttackOn()//공격애니메이터 창에서 추가해주기 (시간대에 실행되도록)
    {
        TriggerAttack = true;
    }
    public void TriggerAttackOff()//공격애니메이터 창에서 추가해주기 (시간대에 실행되도록)
    {
        TriggerAttack = false;
    }

    public void battleFalse() //애니메이션에 추가 배틀이 종료될때 칼이 칼집에 돌아가기
    {
        Weapon.transform.SetParent(WeaponPositionIdle.transform);//아닐경우 등에장착
        Weapon.transform.localPosition = Vector3.zero;
        Weapon.transform.localRotation = Quaternion.Euler(Vector3.zero);
    }


    /// <summary>
    /// 스킬이다스킬
    /// </summary>

    public void Skill5Ins() //점프어택후 폭발
    {
        if (skillsEffect[4] != null) //만약 켜져있는중에 또실행하면?
        {
            skillsEffect[4] = null;
        }
        else if (skillsEffect[4] == null)
        {
            skillsEffect[4] = Instantiate(skillsEffectPrefab[4]);
            skillsEffect[4].transform.SetParent(this.transform);
            skillsEffect[4].transform.localScale = Vector3.one;
            skillsEffect[4].transform.localPosition = new Vector3(0, 2f, 1f);
            skillsEffect[4].transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }



    public void Skill6Ins()
    {
        if (skillsEffect[5] != null) //만약 켜져있는중에 또실행하면?
        {
            skillsEffect[5] = null;
        }

        if (skillsEffect[5] == null) //else 붙여야됨 
        {
            skillsEffect[5] = Instantiate(skillsEffectPrefab[5]);
           // skillsEffect[5].transform.SetParent(this.transform);
            skillsEffect[5].transform.localScale = Vector3.one;
            skillsEffect[5].transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y+1f, this.transform.position.z+1f);
            skillsEffect[5].transform.localRotation = this.transform.rotation;//Quaternion.Euler(0, 0, 0);
           // skillsEffect[5].transform.TransformDirection = Quaternion.Euler(this.transform.position);
        }

    }
    
    public void MinusMp()
    {
        currentMp += -10;

        
    }


    public void SlashIns1()
    {
        if (SlashEffect1 == null)
        {
            SlashEffect1 = Instantiate(SlashEffectPrefab1);
            SlashEffect1.transform.SetParent(this.transform);
            SlashEffect1.transform.localScale = Vector3.one;
            SlashEffect1.transform.localPosition = Vector3.zero;
            SlashEffect1.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }




    



    public void SaveButton()
    {
        alarmText("게임이 저장되었습니다.");
        SaveControll.SaveData();

    }
    public void MainReturnButton()
    {
        Application.LoadLevel("Level 00");
    }


    public void ReturnButton()
    {
        ExitPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }



    public void alarmText(string data)
    {
        AlarmPanel.SetActive(true);
        AlarmPanel.GetComponentInChildren<Text>().text = data;
        alarmTimer = 0;
    }

    

}



/* 공속
Animator anim = GetComponent<Animator>(); 
anim.speed = 1.0f;
*/
