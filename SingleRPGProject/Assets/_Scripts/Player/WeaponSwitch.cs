using UnityEngine;
using System.Collections;

public class WeaponSwitch : MonoBehaviour {
    GameObject weaponObject;//장착
    GameObject weaponPrefab;//리소스폴더에있는 prefab weapon 오브젝트
    GameObject child;
    
    InventoryScript EquipWeaponData; //아이템 데이터를 가져올 스크립트 변수

    int currentWeaponID;
    
    // Use this for initialization
    void Start () {
        EquipWeaponData = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();
        

        if (EquipWeaponData.items[0].ID == -1) //존재하지 않으면
        {
            return;
        }
        else
        {
            weaponPrefab = Resources.Load<GameObject>("Weapon/" + EquipWeaponData.items[0].Slug) as GameObject;
            weaponObject = GameObject.Find("Weapon");
            currentWeaponID = EquipWeaponData.items[0].ID;
            //Instantiate(weaponPrefab);
            child = Instantiate(weaponPrefab) as GameObject;
            child.transform.SetParent(weaponObject.transform);
            //weaponPrefab.transform.parent = weaponObject.transform;


            child.transform.localScale = Vector3.one;
            child.transform.localPosition = Vector3.zero;
            child.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
        //child.transform.localPosition = new Vector3(-0.013f, 0.003f, 0.003f);
       // child.transform.localRotation = Quaternion.Euler(0, 262, 270);
        
	
	}
	
    public void weaponSwitch(itemClass item)
    {
        if (item.ID == -1) //존재하지 않으면
        {

            currentWeaponID = -1;//존재하지않을때 currentweaponID 초기화 
            Destroy(child);
            return;
        }
        else //무기가 존재하면
        {

            //Debug.Log("hh");
            Destroy(child);//이전의 아이템은 삭제
            weaponPrefab = Resources.Load<GameObject>("Weapon/" + item.Slug) as GameObject;
            weaponObject = GameObject.Find("Weapon");
            currentWeaponID = EquipWeaponData.items[0].ID;
            //Instantiate(weaponPrefab);
            child = Instantiate(weaponPrefab) as GameObject;
            child.transform.SetParent(weaponObject.transform);
            //weaponPrefab.transform.parent = weaponObject.transform;


            child.transform.localScale = Vector3.one;
            child.transform.localPosition = Vector3.zero;
            child.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }
	// Update is called once per frame
    /*
	void Update () {
      

        if (currentWeaponID != EquipWeaponData.items[0].ID)//만약 아이템의 아이디가 변경되면 
        {
           
            if (EquipWeaponData.items[0].ID == -1) //존재하지 않으면
            {
             
                currentWeaponID = -1;//존재하지않을때 currentweaponID 초기화 
                Destroy(child);
                return;
            }
            else //무기가 존재하면
            {
               
                //Debug.Log("hh");
                Destroy(child);//이전의 아이템은 삭제
                weaponPrefab = Resources.Load<GameObject>("Weapon/" + EquipWeaponData.items[0].Slug) as GameObject;
                weaponObject = GameObject.Find("Weapon");
                currentWeaponID = EquipWeaponData.items[0].ID;
                //Instantiate(weaponPrefab);
                child = Instantiate(weaponPrefab) as GameObject;
                child.transform.SetParent(weaponObject.transform);
                //weaponPrefab.transform.parent = weaponObject.transform;


                child.transform.localScale = Vector3.one;
                child.transform.localPosition = Vector3.zero;
                child.transform.localRotation = Quaternion.Euler(0, 0, 0);
            }

        }
      

        //Debug.Log(weaponObject);    
    }*/
}
