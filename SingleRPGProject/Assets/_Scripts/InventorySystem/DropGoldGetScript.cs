using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DropGoldGetScript : MonoBehaviour, IPointerDownHandler{

    DropItemOnScript DropItemSc; // false시킬 스크립트 가져오기
    InventoryScript inven;
    Transform player;

    public int dropGoldId; // ui 텍스트 이미지를 설정할 id 변수
    public int Goldamount;



    // Use this for initialization
    void Start()
    {
        DropItemSc = GameObject.Find("InventoryObject").GetComponent<DropItemOnScript>();
        inven = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();
        player = GameObject.Find("Player").GetComponent<Transform>();

     

        DropItemSc.remainGold--;
    }
    
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (!player.GetComponent<PlayerControll>().isDead)//죽으면 안주워지게
        {
            if (Vector3.Distance(player.position, DropItemSc.insGoldObj[dropGoldId].transform.position) < 3) //거리가 가까워야 먹어짐
            {
                inven.Addgold(Goldamount);//saveitemid는 떨어져있는 아이템을 주웟을때 들어와야할 아이템의 직접적인 아이디이고 dropitemid는 변수ui 텍스트 이미지에 셋팅해둔 id

                //Debug.Log();

                Destroy(DropItemSc.insGoldObj[dropGoldId]); // 주웟으면 드랍아이템 obj파괴
                Destroy(DropItemSc.insDropGoldUI[dropGoldId]);
                

                DropItemSc.remainGold--;

                if (DropItemSc.remainGold == 0) //모두 초기화시켜주기..
                {
                    DropItemSc.Goldnumber = 0;//i리셋
                    DropItemSc.insGoldObj.Clear(); // 남아있는아이템이 없으면 0으로 만들어버림
                    DropItemSc.insDropGoldUI.Clear();
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
