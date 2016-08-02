using UnityEngine;
using System.Collections;

public class DurationEffectTriple : MonoBehaviour {

    GameObject player;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        Destroy(this.gameObject, 5f);//5초후삭제인데
    }

    // Update is called once per frame
    void Update()
    {

        if (player.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).nameHash != Animator.StringToHash("Base Layer.Skill2")) //애니메이션 취소되면 바로 없애주기위함.
        {
            Destroy(this.gameObject);
        }
    }
}
