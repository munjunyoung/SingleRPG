using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    Transform target;
    Vector3 targetPosition;// The position that that camera will be following.
    public float smoothing = 5f;        // The speed with which the camera will be following.
   
    Vector3 offset;                     // The initial offset from the target.
    
    Quaternion dir;
    
    float rotationX;
    float rotationY;

    GameObject player;

    float distance=50.0f;
    float offsetDistacne;

    public float currentAngleX;
    public float currentAngleY;
    float currentAngleZ;

    public float height = 5.0f;
    

    // 변경시 변위
   
    Quaternion newRotation;

    RaycastHit hitPosition;
    bool zoomCheck;


    void Start()
    {
        // Calculate the initial offset.
        //offset = (transform.position - target.position);

        distance=30.0f;
       

        target = GameObject.Find("Player").GetComponent<Transform>();
        currentAngleX = -4.5f;
        currentAngleY = 36.0f;
    }

    void FixedUpdate()
    {

     
        //Debug.Log(Quaternion.Euler(0, currentAngleY, 0));



        //  Vector3 targetCamPos = target.position + offset;
        // transform.position = Vector3.Lerp(transform.position, targetCamPos, Time.time);


        //Quaternion newRotation = Quaternion.Euler(0, currentAngleY, 0);

        //   currentAngleX = transform.localEulerAngles.x;
        //   currentAngleY = transform.localEulerAngles.y;
        //   currentAngleZ = transform.eulerAngles.z;



        

        if (Input.GetMouseButton(1))
        {

     


            // 이동할 앵글을 회전으로 변환


            currentAngleX += Input.GetAxis("Mouse X");
            currentAngleY -= Input.GetAxis("Mouse Y");

            if (currentAngleY >= 85 )
            {
                currentAngleY = 85;
            }
            else if(currentAngleY<=3)
            {
                currentAngleY = 3;
            }


            target = GameObject.Find("Player").GetComponent<Transform>();
            // 카메라의 포지션 이동
            // newPosition = target.position;

            //transform.Translate(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
        }
        //카메라 자유시점 적용
       newRotation = Quaternion.Euler(currentAngleY, currentAngleX, 0);
        Vector3 newPosition = newRotation * Vector3.forward * distance;
        newPosition = target.position;
        newPosition -= newRotation * Vector3.forward * distance;
        // 최종 이동
        transform.position = newPosition;

        targetPosition = new Vector3(target.transform.position.x, target.transform.position.y+1f, target.transform.position.z);




        transform.LookAt(targetPosition);

        
        /*  float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * 15.0f;
          rotationY += Input.GetAxis("Mouse Y") * 15.0f;
          rotationY = Mathf.Clamp(rotationY, -20.0f, 60.0f);
          transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);*/



       
            if (Input.GetAxis("Mouse ScrollWheel") < 0 && distance < 30)
            {
                distance = distance + 50 * Time.deltaTime;

          
            //
             }
            if (Input.GetAxis("Mouse ScrollWheel") > 0 && distance > 5)
            {
                distance = distance - 50 * Time.deltaTime; ;

             }

        if (Application.loadedLevelName == "Level 03")
        {
            if (Physics.Raycast(this.transform.position, this.transform.forward, out hitPosition, distance - 3f))
            {
                if (hitPosition.collider.tag == "Ground")
                {
                    offsetDistacne = distance;
                    distance = distance - (Vector3.Distance(hitPosition.transform.position, this.transform.position) - 3);
                    zoomCheck = true; //내가 처음설정학 줌거리가 확대되었을때
                }
            }
          /*  else if (!Physics.Raycast(this.transform.position, -this.transform.forward, out hitPosition, offsetDistacne) && zoomCheck == true)
            {
                distance = offsetDistacne;
                zoomCheck = false;
            }*/

        }


    }


 
}



    
