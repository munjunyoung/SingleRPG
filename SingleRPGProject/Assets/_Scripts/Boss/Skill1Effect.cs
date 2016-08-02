using UnityEngine;
using System.Collections;

public class Skill1Effect : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    StartCoroutine(collideroff(0.5f));
	    Destroy(this.gameObject, 3f);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControll>().TakeDamage(100);
        }
    }


    IEnumerator collideroff(float second)
    {
        yield return new WaitForSeconds(second);
        this.GetComponent<SphereCollider>().radius = 0;
    }
}
