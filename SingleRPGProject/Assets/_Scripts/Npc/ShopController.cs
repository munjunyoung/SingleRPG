using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ShopController : MonoBehaviour, IDropHandler
{

    public int id;//아이템의 아이디를 알아오기위해서 선언
    private InventoryScript inven;
    GameObject SellPanel;
    GameObject BuyPanel;

    ItemData droppedItem;
    Text sellText;
    GameObject player;
    

    void Awake()
    {
        inven = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();
        SellPanel = GameObject.Find("SellPanel");
        BuyPanel = GameObject.Find("BuyPanel");
        player = GameObject.Find("Player");
       

    }
    void Start()
    {
 
    }
  

    void Update()
    {
        if (SellPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SellYes();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                SellNo();
            }
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        
        droppedItem = eventData.pointerDrag.GetComponent<ItemData>();//아이템 데이터의 포인트 드래그 정보를 가져옴
        
        if(BuyPanel.activeSelf)
        {
            BuyPanel.SetActive(false);
        }
       
        if (droppedItem.item.Type == "Weapon")
        {
        SellPanel.transform.FindChild("SellText").GetComponent<Text>().text = " 가격 : " + (droppedItem.item.Value/10) + " 판매 ";
        }
        else
        {
        SellPanel.transform.FindChild("SellText").GetComponent<Text>().text = " 가격 : " + (droppedItem.item.Value / 10) +" 개수 :  "+ droppedItem.amount + " 판매 ";
        }
        
        SellPanel.SetActive(true);


    }

    public void SellYes()
    {
        if (droppedItem.item.Type == "Weapon")
        {
            inven.items[droppedItem.slot] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
            droppedItem.slot = -1;
            inven.Addgold(droppedItem.item.Value / 10);
            Destroy(droppedItem.gameObject);
            
        }
        else //포션일떄
        {
            inven.items[droppedItem.slot] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
            droppedItem.slot = -1;
            inven.Addgold((droppedItem.item.Value / 10)*droppedItem.amount);
            Destroy(droppedItem.gameObject);
            player.GetComponent<PlayerControll>().potionNumber = 0;
            
            
        }

      
        SellPanel.SetActive(false);
    }

    public void SellNo()
    {
        SellPanel.SetActive(false);
    }
 
    
}
