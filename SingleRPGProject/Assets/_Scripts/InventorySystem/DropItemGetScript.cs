using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class DropItemGetScript : MonoBehaviour, IPointerDownHandler{
    DropItemOnScript DropItemSc; // false시킬 스크립트 가져오기
    InventoryScript inven;
    Transform player;
   
    public int dropitemId; // ui 텍스트 이미지를 설정할 id 변수
    

    
    // Use this for initialization
    void Start () {
        DropItemSc = GameObject.Find("InventoryObject").GetComponent<DropItemOnScript>();
        inven = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.GetComponent<PlayerControll>().isDead)//죽으면 안주워지게
        {
            if (Vector3.Distance(player.position, DropItemSc.insItemObj[dropitemId].transform.position) < 3) //거리가 가까워야 먹어짐
            {
                if (!inven.checkItemFull())
                {
                    inven.AddItem(DropItemSc.dropitemsData[dropitemId].ID); //saveitemid는 떨어져있는 아이템을 주웟을때 들어와야할 아이템의 직접적인 아이디이고 dropitemid는 변수ui 텍스트 이미지에 셋팅해둔 id

                    //Debug.Log();

                    Destroy(DropItemSc.insItemObj[dropitemId]); // 주웟으면 드랍아이템 obj파괴
                    Destroy(DropItemSc.insDropUI[dropitemId]);


                    //DropItemSc.i--;
                    DropItemSc.remainItem--;

                    if (DropItemSc.remainItem == 0) //모두 초기화시켜주기..
                    {
                        DropItemSc.ItemNumber = 0;//i리셋
                        DropItemSc.insItemObj.Clear(); // 남아있는아이템이 없으면 0으로 만들어버림
                        DropItemSc.insDropUI.Clear();
                    }
                }
                else if(inven.checkItemFull())
                {
                    player.GetComponent<PlayerControll>().alarmText("인벤토리가 꽉찼습니다.");
                }
            }
            else
            {
                player.GetComponent<PlayerControll>().alarmText("거리가 멀어서 주울수 없습니다.");
            }
        }
       // DropItemSc.Deactivate(); // 없어지기
        
    }

}
