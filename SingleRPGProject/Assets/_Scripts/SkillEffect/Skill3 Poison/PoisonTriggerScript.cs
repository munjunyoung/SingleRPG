using UnityEngine;
using System.Collections;

public class PoisonTriggerScript : MonoBehaviour {
    GameObject playerParent;
    GameObject enemyObject;
    EnemyInsControll enemyIns;

    InventoryScript EquipWeaponDamageData;//아이템 데이터를 가져올 변수 


    GameObject attackeffectPrefab;//공격이펙트 프리팹
    GameObject attackeffect;//공격이팩트오브젝트
    GameObject weaponPosition;//부모로 설정할 오브젝트
    // Use this for initialization
    void Start()
    {
        playerParent = GameObject.Find("Player");
        enemyObject = GameObject.Find("Enemy");
        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        EquipWeaponDamageData = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();

        attackeffectPrefab = Resources.Load<GameObject>("SkillEffect/AttackeffectObject") as GameObject;
    }

    void Update()
    {  
        if (EquipWeaponDamageData == null)
        {
            this.gameObject.SetActive(false);
        }

        if (EquipWeaponDamageData.items[0] != null)
        {
            this.gameObject.SetActive(true);
        }

    }

    
    void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Enemy" && playerParent.GetComponent<PlayerControll>().TriggerAttack == true)//검을 통과한것이 적인지 확인함과 동시에 공격상태 확인// 공격을 당한 적의 애니메이션 상태가 데미지를 받고있는상태면 연속타격이 불가하도록함
        {
            enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().takePoison();
            //   enemyIns.EnemyList[other.gameObject.GetComponent<EnemyController>().id].GetComponent<EnemyController>().enemyPoisonState = 1 ;//적상태를 독으로 만들어줌
        }
        else if(other.tag=="Boss" && playerParent.GetComponent<PlayerControll>().TriggerAttack == true)
        {
            other.GetComponent<BossController>().takePoison();
        }
        
    }
    /*
    void OnCollisionEnter(Collision other)
    {
        Debug.Log("hh");
        if (other.transform.tag == "Enemy" && playerParent.GetComponent<PlayerControll>().TriggerAttack == true)//검을 통과한것이 적인지 확인함과 동시에 공격상태 확인// 공격을 당한 적의 애니메이션 상태가 데미지를 받고있는상태면 연속타격이 불가하도록함
        {
            Debug.Log("hh");
            attackeffect = Instantiate(attackeffectPrefab);

            attackeffect.transform.localPosition = other.contacts[0].point;
           
        }
    }*/


}
