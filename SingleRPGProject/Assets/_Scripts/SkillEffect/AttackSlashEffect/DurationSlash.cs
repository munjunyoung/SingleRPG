
using UnityEngine;
using System.Collections;

public class DurationSlash : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        Destroy(this.gameObject, 1.5f);
	    
	}
	
	// Update is called once per frame
	void Update () {

        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.NormalAttack")) //애니메이션 취소되면 바로 없애주기위함.
        {
            Destroy(this.gameObject);
        }


    }
}
