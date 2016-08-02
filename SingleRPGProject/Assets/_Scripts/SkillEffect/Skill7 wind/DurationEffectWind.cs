using UnityEngine;
using System.Collections;

public class DurationEffectWind : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Awake ()
    {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
       if(player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Skill7"))
        {
            Destroy(this.gameObject);
            player.GetComponent<PlayerControll>().skillsEffect[6] = null;
        }
    }
}
