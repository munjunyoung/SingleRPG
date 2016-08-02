using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NpcController : MonoBehaviour {

    GameObject ShopButtonPanel;
    GameObject ShopPanel;
    GameObject BuyPanel;
    GameObject InvenPanel;
    GameObject SellPanel;

    GameObject inven;
    itemClass item;
    ItemDatabase itemdata;

    Text buyText;
    GameObject player;




    // Use this for initialization
    void Awake() {
        inven = GameObject.Find("InventoryObject");
        InvenPanel = GameObject.Find("Inventory Panel");
        itemdata = GameObject.Find("InventoryObject").GetComponent<ItemDatabase>();
        ShopButtonPanel = GameObject.Find("ShopButtonPanel1");
        BuyPanel = GameObject.Find("BuyPanel");
        SellPanel = GameObject.Find("SellPanel");

        player = GameObject.Find("Player");
        ShopPanel = GameObject.Find("ShopPanel");

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {

        if (BuyPanel.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                ClickYes();
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                ClickNo();
            }
        }

    }

 

    public void ButtonClick1()
    {
        if (SellPanel.activeSelf)
        {
            SellPanel.SetActive(false);
        }
        BuyPanel.SetActive(true);

        item = itemdata.FetchItemByID(10);//첫번쨰칸은 포션이므로..
        BuyPanel.transform.FindChild("BuyText").GetComponent<Text>().text = " 가격 : " + item.Value + " 구매 ";
        EventSystem.current.SetSelectedGameObject(null);
        BuyPanel.transform.SetAsLastSibling();

    }
    public void ButtonClick2()
    {
        if (SellPanel.activeSelf)
        {
            SellPanel.SetActive(false);
        }
        BuyPanel.SetActive(true);

        item = itemdata.FetchItemByID(1);//첫번쨰칸은 포션이므로..
        BuyPanel.transform.FindChild("BuyText").GetComponent<Text>().text = " 가격 : " + item.Value + " 구매 ";
        EventSystem.current.SetSelectedGameObject(null);
        BuyPanel.transform.SetAsLastSibling();

    }
    public void ButtonClick3()
    {
        if (SellPanel.activeSelf)
        {
            SellPanel.SetActive(false);
        }
        BuyPanel.SetActive(true);

        item = itemdata.FetchItemByID(2);//첫번쨰칸은 포션이므로..
        BuyPanel.transform.FindChild("BuyText").GetComponent<Text>().text = " 가격 : " + item.Value + " 구매 ";
        EventSystem.current.SetSelectedGameObject(null);
        BuyPanel.transform.SetAsLastSibling();

    }
    public void ButtonClick4()
    {
        if (SellPanel.activeSelf)
        {
            SellPanel.SetActive(false);
        }
        BuyPanel.SetActive(true);

        item = itemdata.FetchItemByID(0);//첫번쨰칸은 포션이므로..
        BuyPanel.transform.FindChild("BuyText").GetComponent<Text>().text = " 가격 : " + item.Value + " 구매 ";
        EventSystem.current.SetSelectedGameObject(null);
        BuyPanel.transform.SetAsLastSibling();

    }
    public bool checkPotion()
    {
        for(int i=0;i<50;i++)
        {
            if(inven.GetComponent<InventoryScript>().items[i].ID==10)
            {
                return true;
            }
            
        }
        return false;
    }

    public void ClickYes()
    {
        if (inven.GetComponent<InventoryScript>().playerGold >= item.Value)
        {
            if (inven.GetComponent<InventoryScript>().checkItemFull()) //아이템이 꽉찼을때
            {
                if (item.Stackable == true && checkPotion()) //포션이면 구매가능하고 아니면 꽉찼다고 이야기함 포션이 아이템 장비안에 있는지도 확인
                {
                    inven.GetComponent<InventoryScript>().Addgold(-item.Value); //플레이어 골드 업뎃
                    inven.GetComponent<InventoryScript>().AddItem(item.ID);
                    BuyPanel.SetActive(false);
                }
                else
                {
                   player.GetComponent<PlayerControll>().alarmText("인벤토리가 가득 찼습니다");
                }
            }
            else if(!inven.GetComponent<InventoryScript>().checkItemFull())
            {
                inven.GetComponent<InventoryScript>().Addgold(-item.Value); //플레이어 골드 업뎃
                inven.GetComponent<InventoryScript>().AddItem(item.ID);
                BuyPanel.SetActive(false);
            }
        }
        else
        {
            player.GetComponent<PlayerControll>().alarmText("골드가 부족합니다");
        }

    }

    public void ClickNo()
    {
        BuyPanel.SetActive(false);
    }




}
