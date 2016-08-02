using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour {
    int damage;

    GameObject playerParent;
    GameObject enemyObject;
    EnemyInsControll enemyIns;

    GameObject attackeffectPrefab;//공격이펙트 프리팹
    GameObject attackeffect;//공격이팩트오브젝트
    GameObject weaponPosition;//부모로 설정할 오브젝트
    GameObject boss;

    int currentWeaponID=0;

    InventoryScript EquipWeaponDamageData;//아이템 데이터를 가져올 변수 
    // Use this for initialization
    void Start()
    {
        playerParent = GameObject.Find("Player");
        enemyObject = GameObject.Find("Enemy");
        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        boss = GameObject.Find("Boss");
        GameObject.Find("InventoryObject").GetComponent<InventoryScript>().enabled = true;
      
        attackeffectPrefab = Resources.Load<GameObject>("SkillEffect/AttackeffectObject") as GameObject;

      

    }

    void OnLevelWasLoaded(int level)
    {
        if(level==4)
        {
            boss = GameObject.Find("Boss");
        }
    }

    public void DamageSwitch(itemClass item)
    {
        damage = item.Power+(int)(playerParent.GetComponent<PlayerControll>().attackPower);
     
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && playerParent.GetComponent<PlayerControll>().TriggerAttack == true
         && enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().isDead == false)//검을 통과한것이 적인지 확인함과 동시에 공격상태 확인// 공격을 당한 적의 애니메이션 상태가 데미지를 받고있는상태면 연속타격이 불가하도록함
        {

          
                
            enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().TakeDamage(damage);
            attackeffect = Instantiate(attackeffectPrefab);

            //   p = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            //  attackeffect.transform.localPosition = p;


            Vector3 p;
            p = this.gameObject.GetComponentInChildren<MeshCollider>().ClosestPointOnBounds(other.transform.position);
            float distance = Vector3.Distance(p, other.transform.position);
           
            
            attackeffect.transform.localPosition = p;
            attackeffect.transform.localRotation = other.transform.rotation;
        }
        else if(other.tag=="Boss"&& playerParent.GetComponent<PlayerControll>().TriggerAttack == true)
        {
            boss.GetComponent<BossController>().TakeDamage(damage);

            attackeffect = Instantiate(attackeffectPrefab);

            //   p = new Vector3(other.transform.position.x, other.transform.position.y + 1, other.transform.position.z);
            //  attackeffect.transform.localPosition = p;


            Vector3 p;
            p = this.gameObject.GetComponentInChildren<MeshCollider>().ClosestPointOnBounds(other.transform.position);
            float distance = Vector3.Distance(p, other.transform.position);


            attackeffect.transform.localPosition = p;
            attackeffect.transform.localRotation = other.transform.rotation;
        }
    }

    /*
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("hh");
        if (other.transform.tag == "Enemy")// && playerParent.GetComponent<PlayerControll>().TriggerAttack == true)//검을 통과한것이 적인지 확인함과 동시에 공격상태 확인// 공격을 당한 적의 애니메이션 상태가 데미지를 받고있는상태면 연속타격이 불가하도록함
        {
            Debug.Log("hh");
            attackeffect = Instantiate(attackeffectPrefab);

            attackeffect.transform.localPosition = other.contacts[0].point;
            enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().TakeDamage(damage);
        }
    }*/

}
