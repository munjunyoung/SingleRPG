using UnityEngine;
using UnityEngine.UI; //UI를 접근해서 사용하기위해 선언해줘야함
using System.Collections;

public class PlayerController : MonoBehaviour
{
    //이동관련
    public float speed = 3f;
    public float turnSpeed = 10f;
    Vector3 movement;
    Rigidbody playerRigidbody;
    Animation anim;

    //달리기 관련
    private float TimeLeft = 0.5f;
    private float Timer = 0.0f;
    public bool attack = false;
    public bool attackRender = false;

    public bool playerAttackRender = false;// 애니메이션 자체에서 이벤트를 주기위함 
    int animSetValue = 1;
    CameraFollow cam;//캠

    // CameraFollow camScript;

    //체력관련
    int playerHealth = 100;
    int currentHealth;
    public bool isDead = false; //플레이어 사망

    Transform Enemy; //
    bool takeD = false; //데미지를 받을때 bool
    bool battleOn; // 데미지를 받거나 할때 배틀 시작 bool;
    float battleTimer = 0.0f;//배틀 시작 타임 10초면 다시  idle로 돌아가게 하기위함

    //UI관련
    public Slider healthSlider;
    public Image damageImage;
    public Text lifeText;
    public float flashSpeed = 1f;//번쩍거리는스피드?
    public Color flashColor = new Color(1f, 0f, 0f, 0.1f);



    void Awake()
    {
       
        currentHealth = playerHealth;

        anim = GetComponent<Animation>();

        playerRigidbody = GetComponent<Rigidbody>();

        cam = GameObject.Find("Main Camera").GetComponent("CameraFollow") as CameraFollow;
        animSetValue = 1;


        //camScript = cam.GetComponent("CameraFollow") as CameraFollow;
    }

    void FixedUpdate()
    {

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool moving = h != 0f || v != 0f;


        if (isDead == false)
        {


            //스페이스바 공격
            if (Input.GetButton("Jump"))//공격
            {
                attack = true;
                moving = false;
                takeD = false;
                battleOn = true;
                battleTimer = 0.0f;

            }
            else if (!Input.GetButton("Jump") && !anim.isPlaying)//버튼이 눌리지 않고 애니메이션도 실행되지않으면 attack을 false로 만들어 멈춤(한번버튼클릭으로 반복되지않게하기 위함)
            {
                attack = false;
                takeD = false;
            }
            else if (moving == true)
            {
                Move(h, v, speed);
                Turning(h, v);
                //attack = false;
                //takeD = false;
            }


            //위버튼 연속클릭시 달리기 
            if (animSetValue == 1)
            {
                if (Input.GetKeyUp(KeyCode.UpArrow)) //위버튼업 타이머 설정
                {
                   
                    Timer = Time.time;
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {

                    if (Time.time < Timer + TimeLeft && Timer > 0) //버튼을 올리고 다시눌를때까지의 시간차이에 따라서 달리는것 설정
                    {
                        speed = 10;
                        animSetValue = 2;
                        Timer = 0;

                    }
                    else if (Timer == 0)
                    {
                        speed = 3;
                        animSetValue = 1;
                        Timer = 0;
                    }
                    else
                    {
                        speed = 3;
                        animSetValue = 1;
                        Timer = 0;
                    }
                }
            }



            //배틀상태가 온됫을떄 idle설정 타이머
            if (battleOn == true)//배틀상태가 on됫을때
            {
                battleTimer += Time.deltaTime; //배틀 타이머 시작
            }

            if (battleTimer >= 10.0f) //배틀타이머 리셋 
            {
                battleTimer = 0.0f;
                battleOn = false;
            }




            //데미지입었을대 UI 이펙트
            if (takeD)
            {
                damageImage.color = flashColor;
            }

            else
            {
                damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
            }

            
        } // dead if문 끝


        //체력바 설정 0보다클때 아니면 없음
        if (currentHealth >= 0)
        {
            healthSlider.maxValue = playerHealth;
            lifeText.text = currentHealth + "/" + playerHealth; //life 설정 
        }
        else
        {
            lifeText.text = "0/" + playerHealth; //life 설정 
        }

        Animating(animSetValue, moving, attack);



    }


    void Move(float h, float v, float speed)
    {

        // 카메라의 포지션 이동
        // newPosition = target.position;

        //transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);

        //카메라 자유시점 적용


        Quaternion newRotation = Quaternion.Euler(0, cam.currentAngleX, 0);

        movement.Set(h, 0f, v);
        Vector3 newPosition = newRotation * movement;

        newPosition = newPosition.normalized * speed * Time.deltaTime;
        //movement = new Vector3(h, 0.0f,v);

        playerRigidbody.MovePosition(transform.position + newPosition);
        // playerRigdbody.AddForce(movement * speed);

    }

    void Turning(float h, float v)
    {
        Quaternion Rotation = Quaternion.Euler(0, cam.currentAngleX, 0);
        Vector3 _Direction = new Vector3(h, 0.0f, v);
        Vector3 newDirection = Rotation * _Direction;
        Quaternion _Rotation = Quaternion.LookRotation(newDirection);
        Quaternion newRotation = Quaternion.Lerp(playerRigidbody.rotation, _Rotation, turnSpeed * Time.deltaTime);//시작값과 나중값을 넣은후에 
        playerRigidbody.MoveRotation(newRotation);

    }


    public void TakeDamage(int damageAmount) //공격을 받을때
    {
        Enemy = GameObject.Find("Enemy").GetComponent<Transform>();
        if (isDead)
        {
            return;
        }
        takeD = true;//데미지받는 변수 트루전환
        battleOn = true;//배틀 시작 변수 트루전환
        battleTimer = 0.0f;//맞을때마다 타이머를 리셋시켜줌
        currentHealth -= damageAmount;//피깎
        if (Enemy != null)
        {
            transform.LookAt(Enemy);//때린적을 바라보기
        }
        healthSlider.value = currentHealth;//슬라이더값과 최근 플레이어 피값 넣기
        anim.CrossFade("resist", 0.25f);

        if (currentHealth <= 50) //색깔변경?
        {

        }

        if (currentHealth <= 0)
        {
            Death();
        }
        

    }


    void Death() //죽음
    {
        isDead = true;
        anim.CrossFade("die", 0.25f);

    }

    void Animating(int set, bool move, bool attack)
    {
        if (isDead == false)//안죽었을때만 이것들을 시행
        {

            if (attack == true)
            {
                anim.CrossFade("attack", 0.01f);
                speed = 3.0f;//공격시 스피드를 다시 원상복귀
                animSetValue = 1;
            }
            else if (move == true)
            {
                if (set == 1)
                {
                    anim.CrossFade("walk", 0.5f);
                }
                else if (set == 2)
                {
                    anim.CrossFade("run", 0.25f);
                }
            }
            else if (takeD == true)
            {
                // anim.CrossFade("resist", 0.25f);
            }
            else
            {
                if (battleOn == true) //공격당해서 배틀온상태일때
                {
                    anim.CrossFade("idlebattle", 0.25f);
                }
                else
                {
                    anim.CrossFade("idle", 0.25f);
                }
                speed = 3; // 달리기 원상복귀
                animSetValue = 1; //달리기 원상복귀 값
            }

        }


    }

    //공격모션을 시작하자마자 공격이 들어가지않도록 
    public void attackRenderOn()//애니메이터 창에서 추가해주기 (시간대에 실행되도록)
    {
        attackRender = true;
    }
    public void attackRenderOff()//애니메이터 창에서 추가해주기 (시간대에 실행되도록)
    {
        attackRender = false;
    }




}






///////////////////////////////////

/*
   


    /////////////////////////////////////////////////























/* 키눌러야 빨라지는 코드
     if (Input.GetKeyDown(KeyCode.LeftShift))
     {
         speed = 10;
         animSetValue = 2;

     }
     else if(Input.GetKeyUp(KeyCode.LeftShift))
     {
         speed = 3;
         animSetValue = 1;
     }*/


/*

    RaycastHit hit;
    public Animation anim;
    Vector3 click;
    public float speed = 2f;
    public float turnSpeed = 2f;
  

    bool isMove;

    void Awake()
    {
       // anim = GetComponent<Animation>();
      
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1)) // mousebutton(0) 0인 이유? 마우스 왼쪽클릭 1이 오른쪽 2가 가운데 클릭 -결과값은 true false로만 나옴 그러므로 true가 들어올시 
        {
            //camera.main 은 메인카메라 tag가져온것 마우스포지션을 가져옴if로 클릭했을대만 걸었기때문에 클릭한포지션을 가져옴
            //out Hit은 hitInfo	참이 반환될 경우, hitInfo변수는 어디에서 컬라이더가 충돌하였는지에 관한 자세한 정보를 포함할 것입니다. (참고: RaycastHit).
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
            {
                click = hit.point;// 3d로 바뀐좌표를 vector값을 저장함
                
               
                isMove = true;
            }



        }

        //transform.position += dir * speed * Time.deltaTime;
        //transform.Translate((click - transform.position).normalized * speed * Time.smoothDeltaTime); // 마우스포인터의 벡터값과 현재 캐릭위치의 백터값을 뺴면 방향의 벡터를 구함 (normalized를해줌으로써 크기는 일정해지고 방향만을 구해짐)
        //time.delttime을 줌으로써 자연스럽게 움직이게함(원래는 큼직큼직하게움직인다함??)
        // playerRigidbody.MovePosition((click - transform.position).normalized * speed * Time.smoothDeltaTime);
        if (isMove == true)
        {
            Move();
            anim.Play("walk");
            
        }
        else if (isMove == false)
        {
           anim.Play("idle");
        }



    }

    void Move()
    {

        transform.Translate((click - transform.position).normalized * speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, click) <= 0.1f && Vector3.Distance(transform.position, click) >= -0.1f)
        {
            isMove = false;
            return;
        }

    }
   


   */
