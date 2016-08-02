using UnityEngine;
using System.Collections;

public class DurationLevelUp : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Awake() {
        player = GameObject.Find("Player");
        this.gameObject.GetComponent<SphereCollider>().radius = 0f;
        StartCoroutine(SkillTriggerIncrease(0.2f));
        StartCoroutine(SkillTriggerReduce(0.5f));
        Destroy(this.gameObject, 3f);
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {

           other.GetComponent<EnemyController>().KnockBack(5,player);
            
        }
    }


    IEnumerator SkillTriggerReduce(float second) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);
        this.gameObject.GetComponent<SphereCollider>().radius = 0f;

    }

    IEnumerator SkillTriggerIncrease(float second)
    {
        yield return new WaitForSeconds(second);
        this.gameObject.GetComponent<SphereCollider>().radius = 10f;

    }
    
}
