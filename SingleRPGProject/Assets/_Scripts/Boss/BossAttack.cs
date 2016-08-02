using UnityEngine;
using System.Collections;

public class BossAttack : MonoBehaviour {
    GameObject player;
    GameObject boss;

    private float timer;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        boss = GameObject.Find("Boss");
	}

    void Update()
    {
        if (player.GetComponent<PlayerControll>()._grappedfromEnemy == true)
        {
            timer += Time.deltaTime;
            if (timer > 1)
            {
                player.GetComponent<PlayerControll>().TakeDamage(20);
                timer = 0;
            }
        }

        if (boss.GetComponent<BossController>().Die == true)
        {
            Destroy(this.gameObject);
        }
    }
	
	void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player"&&boss.GetComponent<BossController>().attackTrigger==true)
        {
            player.GetComponent<PlayerControll>().TakeDamage(50);
        }

	    if (boss.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).fullPathHash ==
	        Animator.StringToHash("Base Layer.BossSkill2Before"))
	    {
	        if (other.tag == "Player")
	        {

	            boss.GetComponent<BossController>()._playerGrap = true;

                other.GetComponent<PlayerControll>()._grappedfromEnemy = true;
                
                other.transform.SetParent(this.transform);
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.transform.localPosition =new Vector3(-0.3f, 0.5f,0);
                other.transform.localRotation = Quaternion.Euler(new Vector3(90,0,0));
	            StartCoroutine(grapDamage());

	        }
	    }

    }

    IEnumerator grapDamage()
    {
        yield return new WaitForSeconds(0.5f);
        if (player.GetComponent<PlayerControll>()._grappedfromEnemy == true)
        {
           
        }
    }
}
