using UnityEngine;
using System.Collections;

public class PlayerTurning : MonoBehaviour
{
    /*
    RaycastHit hit;

    Vector3 click;
    public float turnSpeed = 5f;
    Rigidbody playerRigidbody;
    Quaternion dir;
    Quaternion Playerdir;

    bool isTurning;

    bool isMove;

    void Awake()
    {
       playerRigidbody = GetComponent<Rigidbody>();
    
    }

    void FixedUpdate()
    {
        if (Input.GetMouseButton(1)) // mousebutton(0) 0인 이유? 마우스 왼쪽클릭 1이 오른쪽 2가 가운데 클릭 -결과값은 true false로만 나옴 그러므로 true가 들어올시 
        {
            //camera.main 은 메인카메라 tag가져온것 마우스포지션을 가져옴if로 클릭했을대만 걸었기때문에 클릭한포지션을 가져옴
            //out Hit은 hitInfo	참이 반환될 경우, hitInfo변수는 어디에서 컬라이더가 충돌하였는지에 관한 자세한 정보를 포함할 것입니다. (참고: RaycastHit).
        Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
            
         click = hit.point;// 3d로 바뀐좌표를 vector값을 저장함
                           
                                   Vector3 targetDirection = (click - transform.position).normalized;
                                   dir = Quaternion.LookRotation(targetDirection,Vector3.up);

                                   Debug.Log(targetDirection);
                                   Quaternion newRotation = Quaternion.Lerp(playerRigidbody.rotation, dir, turnSpeed * Time.deltaTime);
                                   playerRigidbody.MoveRotation(newRotation);



         
        }

        dir = Quaternion.LookRotation((click - transform.position).normalized);
        dir.x = 0;
        dir.z = 0;

        Debug.Log(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, dir, turnSpeed * Time.deltaTime);


    }*/
}
