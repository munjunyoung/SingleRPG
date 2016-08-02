using UnityEngine;
using System.Collections;

public class EnemyFireScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(0, 0, 5 * Time.deltaTime);
        Destroy(this.gameObject, 10f);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerControll>().TakeDamage(20);
            

            Destroy(this.gameObject);
        }
        else if(other.tag =="Ground")
        {
            Destroy(this.gameObject);
        }

    }
}
