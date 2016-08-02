using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BossController : MonoBehaviour {
    int _maxhealth;
    int currentHealth;

    public bool aggro;
    public bool AttackOn;
    bool isRun;
    public bool Die;
    public bool attackTrigger;
    
    public NavMeshAgent nav;
    Animator anim;

    GameObject player;


    bool IceState = false;
    public int enemyIceState = 0;
    public float IceTimer = 0;
    GameObject IceEffectPrefab;
    GameObject IceEffect;

    //렌더러변경
    Renderer enemyobRenderer;
    Material IceMt;
    Material OriginMt;


    public bool poisonState;
    int poisonStack;
    float poisonTimer;

    GameObject PoisonEffectPrefab;
    GameObject PoisonEffect;
    GameObject bone;

    

    //Health Slider
    GameObject BossHealthPanel;
    float Skill1Timer;


    private GameObject _skill1WarningPrefab;
    private GameObject _Skill1Warning;
    private GameObject _Skill1EffectPrefab;
    private GameObject _Skill1Effect;
    private int skill1=0;
    //skill2 그랩
    public bool _playerGrap;

    private GameObject popup;
    private GameObject popupPrefab;
    private GameObject popupParent;


    private GameObject _bossDeathEffectPrefab;
    private GameObject _bossDeathEffect;


    private GameObject dontransform;

    // Use this for initialization
    void Start () {
        nav = this.GetComponent<NavMeshAgent>();
        anim = this.GetComponent<Animator>();
        player = GameObject.Find("Player");
        bone = GameObject.Find("BossPoisonPosition");

        PoisonEffectPrefab = Resources.Load<GameObject>("SkillEffect/PoisonEffect");
        IceEffectPrefab = Resources.Load<GameObject>("SkillEffect/IceEffect");

        //ice setting 이지만 사용안함
        enemyobRenderer = this.gameObject.transform.FindChild("mesh").GetComponent<Renderer>();
        IceMt = Resources.Load<Material>("_Material/Ice");
        OriginMt = Resources.Load<Material>("_Material/Troll_DIF");

        dontransform = GameObject.Find("DonDestroyObject(Clone)");
        _maxhealth = 1500;
        currentHealth = _maxhealth;

        BossHealthPanel = GameObject.Find("BossHealthPanel");
        BossHealthPanel.GetComponentInChildren<Slider>().maxValue = _maxhealth;
        BossHealthPanel.GetComponentInChildren<Slider>().value = currentHealth;
        BossHealthPanel.GetComponentInChildren<Text>().text = "" + currentHealth;

        
        BossHealthPanel.SetActive(false);

        _skill1WarningPrefab = Resources.Load<GameObject>("SkillEffect/bossSkill/BossSkill1WarningEffect") as GameObject;
        _Skill1EffectPrefab = Resources.Load<GameObject>("SkillEffect/bossSkill/BossSkill1Effect") as GameObject;

        _bossDeathEffectPrefab = Resources.Load<GameObject>("SkillEffect/bossSkill/BossDeathEffect");

        popupPrefab = Resources.Load<GameObject>("SkillEffect/PopUpEffect");
        popupParent = GameObject.Find("PopUpObject");
       
    }
	
	// Update is called once per frame
	void Update () {
        if (!Die)
        {
            if (aggro == true)
            {
                Aggro();
                
                
            }

            


            Poison();
            
            AnimationSet();
        }
	}




    void BossSkill1()
    {
            isRun = false;
            AttackOn = false;
            anim.SetTrigger("Skill1");
    }

    void BossSkill2()
    {
        isRun = false;
        AttackOn = false;
        anim.SetTrigger("Skill2");
    }



    void Aggro()
    {
        if (Vector3.Distance(player.transform.position, this.transform.position) > 3)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.Run"))
            {
                nav.SetDestination(player.transform.position);
            }
            else
            {
                nav.ResetPath();
            }
                isRun = true;
                AttackOn = false;
        }
        else
        {
            nav.ResetPath();
            AttackOn = true;
            isRun = false;


            Skill1Timer += Time.deltaTime;
            if (Skill1Timer > 7) // 공격하고있을때
            {
                BossSkill2();
                Skill1Timer = 0;
            }

        }

    }

    /*
    void Ice()
    {
        if (enemyIceState == 1) //장판위에 계속접총중일때
        {

            IceTimer = 0; //얼음에 걸려있는동안은 계속스피드가 1

            IceState = true;
            if (IceEffect == null)
            {
                IceEffect = Instantiate(IceEffectPrefab);
                IceEffect.transform.SetParent(this.transform);
                IceEffect.transform.localPosition = Vector3.zero;
                IceEffect.transform.localScale = Vector3.one * 10;
                enemyobRenderer.material = IceMt;
                anim.speed = 0.5f;

            }
            /* if(IceTimer>5)//--장판위에 적이있는상태에서 오브젝트사라짐..
             {
                 enemyIceState = 0;
             }
        }

        else if (enemyIceState == 0)//접촉이풀렸을때
        {
            IceTimer += Time.deltaTime;
            if (IceTimer > 5)
            {
                enemyobRenderer.material = OriginMt;
                Destroy(IceEffect);
                IceEffect = null;
                IceState = false;
                anim.speed = 1.0f;
                enemyIceState = -1; //계속진입하는걸방지

            }
        }

    }*/


    void Poison()
    {
        if (poisonState == true)//독에 걸린상태라면
        {
         
            poisonTimer += Time.deltaTime;//시간추가..
            if (poisonTimer > 2.0f)
            {

                poisonStack++;
                TakeDamage(10);
                poisonTimer = 0;
            
            }

            if (poisonStack > 3)
            {
              
                poisonTimer = 0;
                poisonState = false;
                Destroy(PoisonEffect);
            }
        }
    }


    public void takePoison()
    {
       // Instantiate(PoisonEffectPrefab);
        if (PoisonEffect == null)
        {
            PoisonEffect = Instantiate(PoisonEffectPrefab);

            PoisonEffect.transform.SetParent(bone.transform);
           
            PoisonEffect.transform.localPosition = new Vector3(0, 0f, 0);
            PoisonEffect.transform.localScale = Vector3.one*7;

            //   enemyPoisonState = 0;
            poisonState = true;
            poisonStack = 0;
        }

    }




    void AnimationSet()
    {
        anim.SetBool("IsRun", isRun);
        anim.SetBool("AttackOn", AttackOn);
    }

    public void TakeDamage(int amount)
    {
        if(Die==true)
        {
            return;
        }
        
        currentHealth += -amount;
        if (!BossHealthPanel.activeSelf)
        {
            BossHealthPanel.SetActive(true);
        }
      
        if (amount > 200)//100이상의 데미지일때만 
        {
            anim.SetTrigger("TakeDamage");
        }

        popup = Instantiate(popupPrefab);
        popup.transform.SetParent(popupParent.transform);
      
        popup.transform.localScale = Vector3.one;

        popup.transform.position = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x, this.transform.position.y + 5f, this.transform.position.z));
        popup.GetComponent<FloatingText>().SetText("-" + amount.ToString());





        if ((int)(((float)currentHealth / (float)_maxhealth) * 100) <= 70 && skill1 == 0)
        {
            BossSkill1();
            skill1 = 1;
        }
        else if ((int)(((float)currentHealth / (float)_maxhealth) * 100) <= 30 && skill1 == 1)
        {
            BossSkill1();
            skill1 = 2;
        }



        if (currentHealth <= 0)
        {
            Die = true;
            currentHealth = 0;
            Death();
        }
        BossHealthPanel.GetComponentInChildren<Slider>().value = currentHealth;
        BossHealthPanel.GetComponentInChildren<Text>().text = "" + currentHealth + "   " + (int)(((float)currentHealth / (float)_maxhealth) * 100) + "%";

    }

    void Death()
    {
        dropSetting();

        BossSkill2GrapOff();
        GetComponent<CapsuleCollider>().isTrigger = true;
        GetComponent<Rigidbody>().isKinematic = true;// 가라앉히기 위해서
        anim.SetTrigger("Death");


        _bossDeathEffect = Instantiate(_bossDeathEffectPrefab);
        _bossDeathEffect.transform.SetParent(this.transform);
        _bossDeathEffect.transform.localPosition = Vector3.zero;
    }




    void dropSetting()
    {
        
        DropItemOnScript drop;
        drop = GameObject.Find("InventoryObject").GetComponent<DropItemOnScript>();
       
            if (Random.Range(0, 100) < 100)
            {
                // drop.DropItem(Random.Range(0, 2), DeathPosition);
                drop.DropItem(1, this.transform.position); //범위값이 인트값인경우 최대값이 나오지않으므로 3까지해두면됨
            }


        for (int i = 0; i < Random.Range(5, 15); i++)
        {
            drop.DropGold(Random.Range(100, 1000), this.transform.position);
        }

    }








    //animation 에서 이벤트로 추가해줄함수
    void BossSkill1beforEffectins()
    {
        _Skill1Warning = Instantiate(_skill1WarningPrefab);
        _Skill1Warning.transform.SetParent(this.gameObject.transform);
        _Skill1Warning.transform.localPosition = Vector3.zero;
        _Skill1Warning.transform.localScale = Vector3.one;

    }

    void BossSkill1Effectins()
    {
        _Skill1Effect = Instantiate(_Skill1EffectPrefab);
        _Skill1Effect.transform.SetParent(this.gameObject.transform);
        _Skill1Effect.transform.localPosition = Vector3.zero;
        _Skill1Effect.transform.localScale = Vector3.one;
    }


    void BossSkill2Grap()
    {
        anim.SetBool("GrapPlayer",_playerGrap);
    }

    void BossSkill2GrapOff()
    {
      
        _playerGrap = false;
        anim.SetBool("GrapPlayer", _playerGrap);
        player.GetComponent<PlayerControll>()._grappedfromEnemy = false;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.transform.SetParent(dontransform.transform);

        player.GetComponent<PlayerControll>().TakeDamage(50);
        player.transform.LookAt(this.transform);
        player.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    void BossSkill1Grapoff()
    {
        if (_playerGrap == true)
        {
            _playerGrap = false;
            anim.SetBool("GrapPlayer", _playerGrap);
            player.GetComponent<PlayerControll>()._grappedfromEnemy = false;
            player.GetComponent<Rigidbody>().isKinematic = false;
            player.transform.SetParent(dontransform.transform);
        }
    }

    void attackTriggerOn()
    {
        attackTrigger = true;
    }
    void attackTriggerOff()
    {
        attackTrigger = false;
    }
    void lookPlayer()
    {
        this.transform.LookAt(player.transform);
    }


   
  
}
