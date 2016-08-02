using UnityEngine;
using System.Collections;

public class IceTriggerScript : MonoBehaviour {

    GameObject playerParent;
  
    EnemyInsControll enemyIns;

  

    // Use this for initialization
    void Awake()
    {
        playerParent = GameObject.Find("Player");
       
        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        

      
    }

    void Update()
    {
      

    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().enemyIceState = 1;//적상태를 얼음으로
        }
        else if(other.tag == "Boss")
        {
            other.GetComponent<BossController>().enemyIceState = 1;
        }
    }
    
    void OnTriggerExit(Collider other)
    {
            if (other.tag == "Enemy")
            {
                enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().enemyIceState = 0;//적상태를 dd으로 만들어줌
            }
        else if (other.tag == "Boss")
        {
            other.GetComponent<BossController>().enemyIceState = 1;
        }

    }
}
