using UnityEngine;
using System.Collections;

public class AggroCheck : MonoBehaviour {

     public int id; 
     EnemyController Enemy;
    EnemyInsControll EnemyIns;
    //public Vector3 currentPosition; //어그로가 끌리기전 자기 위치를 저장할 변수 


    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {

            EnemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
            Enemy = EnemyIns.EnemyList[id].GetComponent<EnemyController>();
            //currentPosition = Enemy.transform.position; //움직이기전 자기 위치 저장 
            Enemy.AggroOn = true;
            Enemy.currentPosition = this.transform.position;
         
        }
    }
    
}
