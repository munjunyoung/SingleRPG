using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
    public int enemyAttackDamage = 50;
    public int id;
    EnemyInsControll enemyIns;
    EnemyController enemyParent;//에너미 부모 오브젝트
    PlayerControll playerObject; // 플레이어 체력을 알기위한 오브젝트


	// Use this for initialization
	void Start() {
        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        playerObject = GameObject.Find("Player").GetComponent<PlayerControll>();
        enemyParent = enemyIns.EnemyList[id].GetComponent<EnemyController>();
       
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Player"&&enemyParent.AttackRender==true)//적의 공격 애니메이션에서 트루 폴스 설정해둠 임팩트 순간에 공격이 허용되도록
        {
            playerObject.TakeDamage(enemyAttackDamage);
        }
        
    }

   

    // Update is called once per frame
    void Update () {
	
	}
}
