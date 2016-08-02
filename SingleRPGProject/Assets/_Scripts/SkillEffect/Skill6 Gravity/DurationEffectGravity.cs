using UnityEngine;
using System.Collections;

public class DurationEffectGravity : MonoBehaviour {
    EnemyInsControll enemyIns;
    GameObject Player;

    // Use this for initialization
    void Awake() {
        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        Player = GameObject.Find("Player");
        StartCoroutine(SkillTriggerReduce(14.5f));
        Destroy(this.gameObject, 15f);
        this.GetComponent<SphereCollider>().radius = 4;


	}




    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
           
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //    Debug.Log(enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].gameObject);
            // enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<Rigidbody>().AddForce(this.transform.position-enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.position, ForceMode.Force);
            // enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().nav.speed = 0f;

            // enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.Translate(enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.position.x, enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.position.y, enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.position.z - (1*Time.deltaTime));

            //    enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.localRotation = Quaternion.Euler(enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.position - this.transform.position);
            //    enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].transform.Translate(0, 0, -1 * Time.deltaTime);

            //중력장형성
            
                enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().nav.speed = 2f;
                enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().nav.SetDestination(this.transform.position);
                other.GetComponent<EnemyController>().anim.speed = 0.5f;
            
            if (other.transform.localScale.x > 0.7f)
            {
                enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().transform.localScale = new Vector3(other.transform.localScale.x - (0.05f * Time.deltaTime), other.transform.localScale.y - (0.05f * Time.deltaTime), other.transform.localScale.z - (0.05f * Time.deltaTime));
            }
            if (other.GetComponent<EnemyController>().isDead == false)
            {
                enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().transform.LookAt(Player.transform);
            }
        }
        else if(other.tag == "Boss")
        {
            other.GetComponent<BossController>().nav.speed = 1f;
            other.GetComponent<BossController>().nav.SetDestination(this.transform.position);
            other.GetComponent<BossController>().TakeDamage(50);

        }
    }
    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Enemy")
        {
            other.transform.localScale = Vector3.one;
            other.GetComponent<EnemyController>().KnockBack(70, this.gameObject);
            other.GetComponent<EnemyController>().anim.speed = 1f;
        }
        else if(other.tag =="Boss")
        {
            other.GetComponent<BossController>().TakeDamage(70);
        }
    }

    IEnumerator SkillTriggerReduce(float second) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);
        this.gameObject.GetComponent<SphereCollider>().radius = 0f;
       
    }

}
