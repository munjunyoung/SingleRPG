using UnityEngine;
using System.Collections;

public class BossAggroCheck : MonoBehaviour {

    BossController bossScript;
    private int i;
    void Awake()
    {
        bossScript = GameObject.Find("Boss").GetComponent<BossController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            bossScript.aggro = true;
            //어그로 온 되도록하면됨
        }
    }
}
