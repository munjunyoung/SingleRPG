using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{

    public int id;//아이템의 아이디를 알아오기위해서 선언
    private InventoryScript inven;
    GameObject player;
    GameObject abilityObject;
    void Start()
    {
        inven = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();
        player = GameObject.Find("Player");
        player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inven.items[0]);
        abilityObject = GameObject.Find("CharacterAbilityObject");

    }
    public void OnDrop(PointerEventData eventData)
    {

        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>();//아이템 데이터의 포인트 드래그 정보를 가져옴


     
        if (id==0&&droppedItem.item.Type=="Weapon")//드랍할 곳의 id가 0(장비슬롯이고 내가 드래그한 item의 타입이 weapon일때만
        { 
            if (inven.items[0].ID == -1)//슬롯에 아이템이 없을경우
            {

                inven.items[droppedItem.slot] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
                
                inven.items[0] = droppedItem.item;
                droppedItem.slot = 0;//놓을자리의 슬롯패널의 아이디를 저장
                player.GetComponent<WeaponSwitch>().weaponSwitch(inven.items[0]);
                player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inven.items[0]);
                abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inven.items[0]);
            }
            else if (droppedItem.slot != 0)//아이템이 있을경우 서로의 아이템 슬롯위치를 바꿔주기위함
            {
               
                Transform item = this.transform.GetChild(0);//자식(슬롯패널)의 트랜스폼을 받아옴
               
                item.GetComponent<ItemData>().slot = droppedItem.slot;//드랍할 슬롯값을 받아옴
                item.transform.SetParent(inven.slots[droppedItem.slot].transform);//(부모)슬롯패널의 transform을 받아옴
                item.transform.position = inven.slots[droppedItem.slot].transform.position;//드랍할 슬롯의 포지션을 가져옴
                abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inven.items[0]);

                droppedItem.slot = 0;//원래 아이템이 존재했던 슬롯의 아이디를 받아와서
                droppedItem.transform.SetParent(this.transform);//원래있던 아이템트랜스폼 받아옴
                droppedItem.transform.position = this.transform.position; // 원래있던 포지션을 설정해줌


                //양쪽아이템 내용물을 서로 바꿈
                inven.items[droppedItem.slot] = item.GetComponent<ItemData>().item;//
                inven.items[0] = droppedItem.item;//
                player.GetComponent<WeaponSwitch>().weaponSwitch(inven.items[0]);
                player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inven.items[0]);
                abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inven.items[0]);

            }
        }
        else if(id>0) //드랍할 슬롯의 id가 장비창이 아닐경우에는 상관음슴
        {
            if (droppedItem.slot == 0 )//내가 드래그를 시작한 아이템 슬롯이 0 즉 아이템 장비 일떄
            {
                if (inven.items[id].ID == -1)//슬롯에 아이템이 없을경우
                {
                    inven.items[droppedItem.slot] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
                    inven.items[id] = droppedItem.item;
                    droppedItem.slot = id;//놓을자리의 슬롯패널의 아이디를 저장
                    player.GetComponent<WeaponSwitch>().weaponSwitch(inven.items[0]);
                    player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inven.items[0]);
                    abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inven.items[0]);
                }
                else if (droppedItem.slot != id&&inven.items[id].Type=="Weapon")//아이템이 있을경우 서로의 아이템 슬롯위치를 바꿔주면서 장비무기슬롯은 무조건 무기만 스왑이 가능되도록 하기위함
                {
                    Transform item = this.transform.GetChild(0);//자식(슬롯패널)의 트랜스폼을 받아옴
                    item.GetComponent<ItemData>().slot = droppedItem.slot;//드랍할 슬롯값을 받아옴
                    item.transform.SetParent(inven.slots[droppedItem.slot].transform);//(부모)슬롯패널의 transform을 받아옴
                    item.transform.position = inven.slots[droppedItem.slot].transform.position;//드랍할 슬롯의 포지션을 가져옴

                    droppedItem.slot = id;//원래 아이템이 존재했던 슬롯의 아이디를 받아와서
                    droppedItem.transform.SetParent(this.transform);//원래있던 아이템트랜스폼 받아옴
                    droppedItem.transform.position = this.transform.position; // 원래있던 포지션을 설정해줌


                    //양쪽아이템 내용물을 서로 바꿈
                    inven.items[droppedItem.slot] = item.GetComponent<ItemData>().item;//
                    inven.items[id] = droppedItem.item;//


                }

            }
            if (droppedItem.slot != 0)
            {
                if (inven.items[id].ID == -1)//슬롯에 아이템이 없을경우
                {
                    inven.items[droppedItem.slot] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
                    inven.items[id] = droppedItem.item;
                    droppedItem.slot = id;//놓을자리의 슬롯패널의 아이디를 저장
                }
                else if (droppedItem.slot != id)//아이템이 있을경우 서로의 아이템 슬롯위치를 바꿔주기위함
                {
                    Transform item = this.transform.GetChild(0);//자식(슬롯패널)의 트랜스폼을 받아옴
                    item.GetComponent<ItemData>().slot = droppedItem.slot;//드랍할 슬롯값을 받아옴
                    item.transform.SetParent(inven.slots[droppedItem.slot].transform);//(부모)슬롯패널의 transform을 받아옴
                    item.transform.position = inven.slots[droppedItem.slot].transform.position;//드랍할 슬롯의 포지션을 가져옴

                    droppedItem.slot = id;//원래 아이템이 존재했던 슬롯의 아이디를 받아와서
                    droppedItem.transform.SetParent(this.transform);//원래있던 아이템트랜스폼 받아옴
                    droppedItem.transform.position = this.transform.position; // 원래있던 포지션을 설정해줌


                    //양쪽아이템 내용물을 서로 바꿈
                    inven.items[droppedItem.slot] = item.GetComponent<ItemData>().item;//
                    inven.items[id] = droppedItem.item;//


                }
            }
            
        }
      
    }
    


}


   