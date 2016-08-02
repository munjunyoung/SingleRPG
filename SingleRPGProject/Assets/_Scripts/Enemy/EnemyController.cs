using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;//LIST를 사용하기위함
using System.Collections;

public class EnemyController : MonoBehaviour {
    public string enemyName;
    public int enemyKind;
    public Animator anim;
    public int id; //적의 id
    public int enemyHealth = 100;
    public int currentHealth;
    public int enemyExp;
  
    public int enemyPoisonState = 0;//0이면 평상시 1이면 독상태, 2이면 얼음상태

    public float attackDistance = 2.5f;

    Vector3 spawnPosition;
    EnemyInsControll enemyInsScript; //에너미 인스포지션

    RaycastHit hit;

    //넉백상태 넉백되있는동안 멈춰주기위함
    bool knockbackState = false;
    float knockbackDistanceOffset = 0;
    //독상태
    bool poisonState = false;
    public float poisonTimer = 0;
    float poisonStack = 0;
    GameObject PoisonEffectPrefab;
    GameObject PoisonEffect;
    //얼음상태
    bool IceState = false;
    public int enemyIceState = 0;
    public float IceTimer = 0;
    GameObject IceEffectPrefab;
    GameObject IceEffect;
    
   

    Renderer enemyobRenderer;
    Material IceMt;
    Material OriginMt;




    Transform player;// 플레이어 위치를 알기 위함 네비 목적지설정
    PlayerControll playerState; // 플레이어가 죽었는지 상태확인
    

   

    CapsuleCollider enemyCollider;//죽었을대 trigger로 변환시켜주기위함

    //정찰 변수
    //= new Transform[4];//적이 정찰중일때 웨이포인트를 넣을 배열
    

    Transform[] patrolWayPoints = new Transform[10];
    int wayIndex = 0;
  
    Vector3 SpawnPosition;


    //콜라이더 트리거가 온됬을때 사용할 bool변수들
    public bool isIdle;
    public bool SpawnOn;
    public bool isDead;
    public bool AggroOn;
    public bool AttackOn;
    public bool AttackRender;//공격을 시작하자마자 칼에 걸렸을때를 방지하기위함
    bool PatrolOn;

    public NavMeshAgent nav;

    public Vector3 currentPosition; //어그로가 끌리기전 자기 위치를 저장할 변수 
    Vector3 DeathPosition; //죽었을때 몬스터의 위치 (아이템 드랍 위치를 저장하기위함)

    Animator avatar;
    
    //체력창 슬라이더 변수들
    Slider healthSlider;
    Text lifeText;
    EnemyInsControll healthUI; // 유아이 정보를 받아올 스크립트?

    GameObject popupParent;
    GameObject popupPrefab;
    GameObject popup;


    public float nextAttack;
    public float attackRate=1.0f; // 어택주기



    GameObject enemyFireEffectPrefab;
    GameObject enemyFireEffect;



    // Use this for initialization
    void Start() {
       
        player = GameObject.Find("Player").GetComponent<Transform>();
        playerState = GameObject.Find("Player").GetComponent<PlayerControll>();
        healthUI = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
      
       
        
       //이 포지션을가지고있는 적이 죽으면 그포지션을 넘겨줘서 리스폰되도록 만들기위함

        popupParent = GameObject.Find("PopUpObject");
        popupPrefab = Resources.Load<GameObject>("SkillEffect/PopUpEffect");
        PoisonEffectPrefab = Resources.Load<GameObject>("SkillEffect/PoisonEffect"); //중독 상태 리소스에서 가져옴
        IceEffectPrefab = Resources.Load<GameObject>("SkillEffect/IceEffect");

        patrolWayPoints[0]= GameObject.Find("wayPoint 0").GetComponent<Transform>();
        patrolWayPoints[1]= GameObject.Find("wayPoint 1").GetComponent<Transform>();
        patrolWayPoints[2]= GameObject.Find("wayPoint 2").GetComponent<Transform>();
        patrolWayPoints[3]= GameObject.Find("wayPoint 3").GetComponent<Transform>();
        patrolWayPoints[4] = GameObject.Find("wayPoint 4").GetComponent<Transform>();
        patrolWayPoints[5] = GameObject.Find("wayPoint 5").GetComponent<Transform>();
        patrolWayPoints[6] = GameObject.Find("wayPoint 6").GetComponent<Transform>();
        patrolWayPoints[7] = GameObject.Find("wayPoint 7").GetComponent<Transform>();
        patrolWayPoints[8] = GameObject.Find("wayPoint 8").GetComponent<Transform>();
        patrolWayPoints[9] = GameObject.Find("wayPoint 9").GetComponent<Transform>();
      


        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        enemyCollider = GetComponent<CapsuleCollider>();
        currentHealth = enemyHealth;
        currentPosition = this.transform.position;
        PatrolOn = true;
        AttackOn = false;
        AttackRender = false;
        isDead = false;
        


        healthUI.EnemySliderList[id].transform.FindChild("EnemyHealthSlider").GetComponent<Slider>().maxValue = enemyHealth;
      
        healthUI.EnemySliderList[id].SetActive(false);



        enemyobRenderer = this.gameObject.transform.FindChild("mesh").GetComponent<Renderer>();
        IceMt = Resources.Load<Material>("_Material/Ice");
        if (enemyName == "enemy1")
        {
            OriginMt = Resources.Load<Material>("_Material/skeleton_D");
        }
        else if(enemyName =="enemy2")
        {
            OriginMt = Resources.Load<Material>("_Material/Enemy2_D");
        }
        else if(enemyName=="enemy3")
        {
            OriginMt = Resources.Load<Material>("_Material/Enemy3_D");
        }

        avatar = this.gameObject.GetComponent<Animator>();

        


        enemyInsScript = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        spawnPosition = this.transform.position;

        enemyFireEffectPrefab = Resources.Load<GameObject>("SkillEffect/EnemyFireBall"); //파이어볼 이팩트 소환


        
    }

    // Update is called once per frame
    void FixedUpdate() {


        if (isDead == false)
        {



            if (AggroOn && knockbackState == false) //어그로가 온됫을때 이동
            {
                Aggro();//어그로 함수실행
            }
            else if (AggroOn == false && PatrolOn == false && Vector3.Distance(currentPosition, transform.position) >= 1 && knockbackState == false) //어그로가 풀리고 순찰 온 되고 다시 원래 위치로 돌아가기
            {
                anim.SetBool("IsRun", true);
                anim.SetBool("attacking", false);
                nav.SetDestination(currentPosition);
                if (Vector3.Distance(currentPosition, transform.position) < 2) //어그로가 시작되었을대 저장해둔 포지션과 어느정도 가까워졌으면 순찰 온 
                {
                    PatrolOn = true;
                }

            }
            else if (AggroOn == false && PatrolOn == true && knockbackState == false)//어그로가 다시풀리고 나서 순찰다시시작
            {

                //순찰이 시작되면 몇초후 체력바 사라지게 하기
         
                Patrolling();//순찰함수실행
            }
            else if (knockbackState == true) //넉백애니메이션이 노답이라 만든거
            {
                if (knockbackDistanceOffset < 2.5)
                {
                    this.transform.Translate(0, 0, -0.01f * Time.time);
                    knockbackDistanceOffset += 0.01f * Time.time;
                }
                else if (knockbackDistanceOffset >= 2.5 && knockbackDistanceOffset < 4)
                {
                    this.transform.Translate(0, 0, -5f * Time.deltaTime);
                    knockbackDistanceOffset += 5f * Time.deltaTime;
                }
                else
                {
                    this.transform.Translate(0, 0, -1f * Time.deltaTime);
                }
                // else if(knockbackDistanceOffset)

                nav.speed = 0f;
                nav.ResetPath();
            }

            Ice();
            Poison();//중독상태확인..
         

        }
        else if (isDead == true)
        {
            
            transform.Translate(-Vector3.up * 1f * Time.deltaTime);//시체가 점점 바닥으로 사라지게 하기위함 

            if (knockbackState == true) //넉백애니메이션이 노답이라 만든거
            {
                if (knockbackDistanceOffset < 2.5)
                {
                    this.transform.Translate(0, 0, -0.05f * Time.time);
                    knockbackDistanceOffset += 0.05f * Time.time;
                }
                else if (knockbackDistanceOffset >= 2.5 && knockbackDistanceOffset < 4)
                {
                    this.transform.Translate(0, 0, -5f * Time.deltaTime);
                    knockbackDistanceOffset += 5f * Time.deltaTime;
                }
                else
                {
                    this.transform.Translate(0, 0, -1f * Time.deltaTime);
                }

               
            }
        }

    }



    /// <summary>
    /// //////////////////////////////////////////////////////////////////함수
    /// </summary>
   

    public void KnockBack(int damage, GameObject lookat)
    {
        
            knockbackState = true; //정지시켜주기위함..
            StartCoroutine(knockbackFalse(1.5f));
            //knockbackDistanceOffset = 0; //2번터질때 리셋시켜주기위해서



            transform.LookAt(lookat.transform);
            TakeDamage(damage);
            EnemyStateUI();
        

        if (isDead == false)
        {
            anim.SetTrigger("KnockBack");
        }
     
       
               
           
    }

  


    void Ice()
    {
        if(enemyIceState==1) //장판위에 계속접총중일때
        {
       
            IceTimer = 0; //얼음에 걸려있는동안은 계속스피드가 1
          
            IceState = true;
            if (IceEffect == null)
            {
                IceEffect = Instantiate(IceEffectPrefab);
                IceEffect.transform.SetParent(this.transform);
                IceEffect.transform.localPosition = Vector3.zero;
                enemyobRenderer.material = IceMt;
                anim.speed = 0.5f;

            }
           /* if(IceTimer>5)//--장판위에 적이있는상태에서 오브젝트사라짐..
            {
                enemyIceState = 0;
            }*/
        }
        else if(enemyIceState==0)//접촉이풀렸을때
        {
            IceTimer += Time.deltaTime;
            if(IceTimer>5)
            {
                enemyobRenderer.material = OriginMt;
                Destroy(IceEffect);
                IceEffect = null;
                IceState = false;
                anim.speed = 1.0f;
                enemyIceState = -1; //계속진입하는걸방지
                
            }
        }
    }


    void Poison()
    {
       
        if (poisonState == true)//독에 걸린상태라면
        {
            healthUI.EnemySliderList[id].transform.FindChild("EnemyHealthSlider").gameObject.transform.FindChild("Fill Area").gameObject.transform.FindChild("Fill").GetComponent<Image>().color = Color.green;
            poisonTimer += Time.deltaTime;//시간추가..
            if(poisonTimer>2.0f)
            {
                
                poisonStack++;
                TakeDamage(10);
                poisonTimer = 0;
                EnemyStateUI();
            }
            
            if(poisonStack>3)
            {
                healthUI.EnemySliderList[id].transform.FindChild("EnemyHealthSlider").gameObject.transform.FindChild("Fill Area").gameObject.transform.FindChild("Fill").GetComponent<Image>().color = Color.red;
                poisonTimer = 0;
                poisonState = false;
                Destroy(PoisonEffect);
            }
        }
    }

    public void takePoison()
    {

       // if (enemyPoisonState == 1) //데미지를 받을때마다 포이즌상태를 0으로 바꿔줌
       // {
            PoisonEffect = Instantiate(PoisonEffectPrefab);
            PoisonEffect.transform.SetParent(this.transform);
        PoisonEffect.transform.localPosition = new Vector3(0, 0.5f, 0);

         //   enemyPoisonState = 0;
            poisonState = true;
            poisonStack = 0;

        //}
    }


    public void TakeDamage(int amount)//amount는 유저가공격 하는 데미지양 
    {

        if (isDead)
        {
            return;
        }

        //공격을 당했을시 체력바 실행


        //
        anim.SetBool("attacking", false);
        anim.SetBool("IsRun", false);
        anim.SetBool("IsWalk", false);

        //
        transform.LookAt(player);
        currentHealth -= amount;//공격받으면 체력이 깎임
                                //anim.SetBool("IsDamage", true);

        //데미지 팝업이펙트 생성
        

        popup = Instantiate(popupPrefab);
        popup.transform.SetParent(popupParent.transform);
        popup.transform.localPosition = this.transform.position;
        popup.transform.localScale = Vector3.one;

        popup.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 2f, this.transform.position.z));
        popup.GetComponent<FloatingText>().SetText("-"+amount.ToString());
        
      


        anim.SetTrigger("IsDamage");
        AttackOn = false;
        EnemyStateUI();

        if (currentHealth <= 0 && isDead == false)//위에다 넣지 않은이유는 0==일때가 안됨..
        {
            Death();
        }


    }

    void Death()//에너미 죽음
    {
        anim.SetBool("IsRun", false);
        anim.SetBool("attacking", false);
        anim.SetBool("IsWalk", false);
        AttackOn = false;
        AttackRender = false;
        isDead = true;
        
        anim.SetTrigger("IsDead");


        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;// 가라앉히기 위해서
        nav.ResetPath();

        
        dropSetting();//아이템 드랍
        playerState.UpdateExpSilderUI(enemyExp);//경험치주깅
        
        StartCoroutine(enemyRespawn(12f));
        


        Destroy(healthUI.EnemySliderList[id], 3f);
        Destroy(gameObject, 15f);//5초후 오브젝트 파괴


    }


    public void EnemyStateUI()
    {
        if (isDead == false)
        {
            healthUI.EnemySliderList[id].SetActive(true);
            //체력슬라이더 text 설정
            healthUI.EnemySliderList[id].transform.FindChild("EnemyHealthSlider").GetComponent<Slider>().value = currentHealth;

            if (currentHealth >= 0)
            {
                healthUI.EnemySliderList[id].transform.FindChild("EnemyLifeText").GetComponent<Text>().text = currentHealth + "/" + enemyHealth;

            }
            else
            {
                healthUI.EnemySliderList[id].transform.FindChild("EnemyLifeText").GetComponent<Text>().text = "0/" + enemyHealth;

            }
        }
    

      

    }

    void Aggro()
    {

        if (IceState == false)
        {
            nav.speed = 3.5f;
        }//어그로가 실행됫을대엔 스피드를 5정도로
        else if(IceState==true)
        {
            nav.speed = 1.0f;
        }
      

        if (playerState.isDead == false) //플레이어가 살이있을때만 실행하도록함
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackDistance) //일정거리 이상 가까워졌을때 공격
            {
               if (attackDistance >= 5)
               {
                    this.transform.LookAt(player);
                    if (Physics.Raycast(this.transform.position, this.transform.forward, out hit, attackDistance))
                    {
                       
                        
                        if (hit.collider.tag == "Player") 
                        {
                           
                            anim.SetBool("attacking", true);
                            anim.SetBool("IsRun", false);
                            anim.SetBool("IsWalk", false);

                            AttackOn = true;
                            nav.ResetPath(); //가까워지면 초기화시켜버림
                           
                        }
                        else
                        {
                            anim.SetBool("IsRun", true);
                            anim.SetBool("attacking", false);
                            anim.SetBool("IsWalk", false);
                            nav.SetDestination(player.transform.position);
                            
                            PatrolOn = false;//순찰이 정지되고 케릭을 따라가게 하기위함
                                             //  nav.ResetPath(); nav 셋팅은 모두 공격애니메이션에 넣어둠
                                             // StartCoroutine(navActiveObjInSecond(nav,1.5f)); //1초후에 다시 목적지설정 공격모션을 취하면서 따라가지 않게하기위함
                            if (Vector3.Distance(currentPosition, transform.position) > 30)// 원래위치에서 10이상의 거리를 넘어갔을때에 어그로 풀림
                            {
                                AggroOn = false;
                                nav.ResetPath();
                            }

                        }
                    }

                }
                else
                {
                    anim.SetBool("attacking", true);
                    anim.SetBool("IsRun", false);
                    anim.SetBool("IsWalk", false);

                    AttackOn = true;
                    nav.ResetPath(); //가까워지면 초기화시켜버림
                    transform.LookAt(player);

                }
                
                   
                
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance && AttackOn==false) //거리가 다시멀어지면 이동 일단 시작은 순찰중일떄 바로 따라오게하기위함
            {
                anim.SetBool("IsRun", true);
                anim.SetBool("attacking", false);
                anim.SetBool("IsWalk", false);
                AttackOn = false;
            
                PatrolOn = false;//순찰이 정지되고 케릭을 따라가게 하기위함
                nav.SetDestination(player.transform.position);
                if (Vector3.Distance(currentPosition, transform.position) > 30)// 원래위치에서 10이상의 거리를 넘어갔을때에 어그로 풀림
                {
                    AggroOn = false;
                    nav.ResetPath();
                }
                
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > attackDistance && AttackOn == true)//공격이 온되었을대 바로따라가게 하지않기 위함 
            {
                anim.SetBool("IsRun", true);
                anim.SetBool("attacking", false);
                anim.SetBool("IsWalk", false);
                if (attackDistance > 5)
                {
                    StartCoroutine(attackActiveObjInSecond(2.0f));
                }
                else
                {
                    StartCoroutine(attackActiveObjInSecond(1.0f));//attackon 을 1초후에 false시킴
                }
                PatrolOn = false;//순찰이 정지되고 케릭을 따라가게 하기위함
              //  nav.ResetPath(); nav 셋팅은 모두 공격애니메이션에 넣어둠
               // StartCoroutine(navActiveObjInSecond(nav,1.5f)); //1초후에 다시 목적지설정 공격모션을 취하면서 따라가지 않게하기위함
                if (Vector3.Distance(currentPosition, transform.position) > 30)// 원래위치에서 10이상의 거리를 넘어갔을때에 어그로 풀림
                {
                    AggroOn = false;
                    nav.ResetPath();
                }

            }

            
        }
        else
        {
            AggroOn = false;
        }
    }


  
    void Patrolling() //순찰 함수
    {
        if (IceState == false) //얼음상태면속도늦춰주기..
        {
            nav.speed = 1.5f;
        }
        else if(IceState==true)
        {
            nav.speed = 1.0f;
        }
        anim.SetBool("IsRun", false);
        //anim.SetBool("IsWalk", true);
        AttackOn = false;
      
        //anim.SetTrigger("Walk");

        if (healthUI.EnemySliderList[id].activeSelf) //만약 체력바가 온되있따면 사라지게하기
        {
            if (isDead == false)
            {
                StartCoroutine(SetActiveObjInSecond(healthUI.EnemySliderList[id], 4.5f)); //2초후에 사라지게 하기
            }
        }

      
       
        
            if (!nav.hasPath)
            {
                nav.SetDestination(patrolWayPoints[Random.Range(0, 9)].position);
            }
            else if (Vector3.Distance(patrolWayPoints[wayIndex].position, transform.position) < 3)//웨이포인트 목적지에 이동하면 배열 함수++
            {
                wayIndex = Random.RandomRange(0, 9);
            }
       
       
    }



   void dropSetting()
    {
        DeathPosition = new Vector3(transform.position.x, transform.position.y+2, transform.position.z);//아이템을 드랍시킬 포지션저장
        
        DropItemOnScript drop;
        drop = GameObject.Find("InventoryObject").GetComponent<DropItemOnScript>();
        if (enemyKind == 1)
        {
            if (Random.Range(0, 100) < 10)
            {
                // drop.DropItem(Random.Range(0, 2), DeathPosition);
                drop.DropItem(2, DeathPosition); //범위값이 인트값인경우 최대값이 나오지않으므로 3까지해두면됨
            }

            drop.DropGold(Random.Range(5, 50), DeathPosition);
        }
        else if(enemyKind ==2)
        {
            if (Random.Range(0, 100) < 10)
            {
                // drop.DropItem(Random.Range(0, 2), DeathPosition);
                drop.DropItem(0, DeathPosition); //범위값이 인트값인경우 최대값이 나오지않으므로 3까지해두면됨
            }

            drop.DropGold(Random.Range(30,100), DeathPosition);
        }
        else if (enemyKind == 2)
        {
            if (Random.Range(0, 100) < 10)
            {
                // drop.DropItem(Random.Range(0, 2), DeathPosition);
                drop.DropItem(0, DeathPosition); //범위값이 인트값인경우 최대값이 나오지않으므로 3까지해두면됨
            }

            drop.DropGold(Random.Range(50, 150), DeathPosition);
        }//1이 보라색무기
    }
    
    /// ///////////////////////////
   

    //코루틴 시간 
    IEnumerator SetActiveObjInSecond(GameObject obj, float second) //슬라이더바 시간지나면 active off
    {
      
        yield return new WaitForSeconds(second);
        if (isDead == false)
        {
            obj.SetActive(false);
        }
    }
  

    IEnumerator attackActiveObjInSecond(float second) //공격 bool 변수 시간지나서 false
    {

        yield return new WaitForSeconds(second);
        AttackOn = false;
    }

    IEnumerator knockbackFalse(float second)
    {
        yield return new WaitForSeconds(second);
        knockbackState = false;
        knockbackDistanceOffset = 0;

    }

    IEnumerator navSet(float second)
    {
        yield return new WaitForSeconds(second);

        nav.SetDestination(player.transform.position);
    }


    IEnumerator enemyRespawn(float second)
    {
        yield return new WaitForSeconds(second);

        enemyInsScript.SpawnEnemy(spawnPosition,enemyKind);
    }

    public void navStart()
    {
        if (!isDead)
        {
            nav.SetDestination(player.transform.position);
        }
    }

    public void navStop()
    {
        nav.ResetPath();
    }

    public void attackRenderOn()//애니메이터 창에서 추가해주기 (시간대에 실행되도록)
   {
        AttackRender = true;
    }
    public void attackRenderOff()//애니메이터 창에서 추가해주기 (시간대에 실행되도록)
    {
        AttackRender = false;
    }

    
    public void FireBallIns()
    {
        if (enemyFireEffect!= null) //만약 켜져있는중에 또실행하면?
        {
            enemyFireEffect = null;
        }

        if (enemyFireEffect == null) //else 붙여야됨 
        {
            enemyFireEffect = Instantiate(enemyFireEffectPrefab);
            // skillsEffect[5].transform.SetParent(this.transform);
            enemyFireEffect.transform.localScale = Vector3.one;
            enemyFireEffect.transform.localPosition = new Vector3(this.transform.position.x-0.5f, this.transform.position.y, this.transform.position.z + 0.5f);
            enemyFireEffect.transform.localRotation = this.transform.rotation;//Quaternion.Euler(0, 0, 0);
                                                                              // skillsEffect[5].transform.TransformDirection = Quaternion.Euler(this.transform.position);
        }

    }



}
