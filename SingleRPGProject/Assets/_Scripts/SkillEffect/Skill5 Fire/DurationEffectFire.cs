using UnityEngine;
using System.Collections;

public class DurationEffectFire : MonoBehaviour {
    GameObject playerParent;

    EnemyInsControll enemyIns;
    

    // Use this for initialization
    void Awake()
    {
        playerParent = GameObject.Find("Player");

        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();



    }
    
    void Start()
    {
        Destroy(gameObject, 3f);
        StartCoroutine(SkilltriggerFalse(0.5f)); //애니메이션이 함수를 2번실행하는바람에일단 대책으로 해둠
      
    }


    void Update()
    {


    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().TakeDamage(50);//적상태를 얼음으로

            other.GetComponent<EnemyController>().KnockBack(50, this.gameObject);
        }
        else if (other.tag == "Boss")
        {
            other.GetComponent<BossController>().TakeDamage(50);
        }
    }

    IEnumerator SkilltriggerFalse(float second) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);

        gameObject.GetComponent<Collider>().enabled= false;
    }
}
