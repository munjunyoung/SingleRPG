using UnityEngine;
using System.Collections;
using System.Collections.Generic;//LIST를 사용하기위함
using UnityEngine.UI;//유아이 이미지를 사용하기위함

public class InventoryScript : MonoBehaviour {
    GameObject inventoryPanel;
    GameObject slotPanel;
    GameObject cslotPanel;
    GameObject player;
   
    ItemDatabase database;

    public GameObject GoldText;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    public int playerTotalItemNumber;
 
    public int playerGold=0;
    string goldData;
    
    

    int slotAmount;
    public List<itemClass> items = new List<itemClass>();
    public List<GameObject> slots = new List<GameObject>();

    void Awake()
    {
        database = GetComponent<ItemDatabase>();//스크립트 참조
        slotAmount = 50;
        inventoryPanel = GameObject.Find("Inventory Panel");///name/으로 게임오브젝트를 찾습니다.
        slotPanel = inventoryPanel.transform.FindChild("Slot Panel").gameObject;//비활성화된 오브젝트들을 찾아냄 "slot panel"이란 비활성화된 오브젝트
        cslotPanel = inventoryPanel.transform.FindChild("Character Panel").transform.FindChild("Character Slot Panel").gameObject;
        GoldText = GameObject.Find("Gold Panel").transform.FindChild("GoldText").gameObject;
        player = GameObject.Find("Player");

        goldData = "G : " + playerGold;
        GoldText.GetComponent<Text>().text = goldData;
       
        for (int i = 0; i < slotAmount; i++)//슬롯의 갯수만큼 (슬롯 49개)
        {
            if (i < 1)
            {
                items.Add(new itemClass());//
                slots.Add(Instantiate(inventorySlot));//인스턴스화 시킴
                slots[i].GetComponent<Slot>().id = i;//각각 슬롯의 아이디를 번호순으로 부여함
                slots[i].transform.SetParent(cslotPanel.transform);//slot의 부모를 설정
                slots[i].transform.localScale = Vector3.one;
                slots[i].transform.localPosition = Vector3.zero;
               
            }
            else if(i>=1)
            {
                items.Add(new itemClass());//
                slots.Add(Instantiate(inventorySlot));//인스턴스화 시킴
                slots[i].GetComponent<Slot>().id = i;//각각 슬롯의 아이디를 번호순으로 부여함
                slots[i].transform.SetParent(slotPanel.transform);//slot의 부모를 설정
                slots[i].transform.localScale = Vector3.one;
                slots[i].transform.localPosition = Vector3.zero;

            }//1번째 슬롯은 장비창의 무기 슬롯

        }


    }

    void Start()
    {
        
    }


   



    public void AddItem(int id)
    {
       
        
            itemClass itemToAdd = database.FetchItemByID(id);
            ItemData data;
        
        playerTotalItemNumber++;
      
     //   player.GetComponent<PlayerControll>().itemNumber++;
     
            if (itemToAdd.Type=="Weapon"&&CheckIfWeaponEquip()==false) // 무기타입이 weapon이고 장비에 착용이 되어있지 않을경우
            {
                items[0] = itemToAdd;//받아온데이터를 item에 넣기
                GameObject itemObj = Instantiate(inventoryItem);//nstantiate란 게임 내에서 오브젝트를 새로 생성하는 기능입니다. 괄호 안의 오브젝트를 생성한다는 것



                itemObj.GetComponent<ItemData>().item = itemToAdd;
                itemObj.GetComponent<ItemData>().slot = 0;//슬롯이 몇번째 슬롯인지 저장 
                itemObj.GetComponent<ItemData>().amount = 1;//갯수
                itemObj.transform.SetParent(slots[0].transform);//부모를 지정해준다음 위치가 정확히 장착 위치에 오도록 지역위치를 slot[i]의 위치에 정확히 오도록 아이템 위치를 설정해줌 
                
                itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;//아이템 이미지를 불러오기 위함
                itemObj.transform.localPosition = Vector2.zero;//아이템 데이터 위치의 로컬 포지션 을 0 로 만듦으로서 그자리에 바로 들어가게만들기위함
                itemObj.transform.localScale = Vector3.one; 
                itemObj.name = itemToAdd.Title;
                
            }
            else if (itemToAdd.Stackable && CheckIfItemIsInInventory(itemToAdd))//갯수처럼 쌓아올릴수 있는 아이템인지 단일템인지 확인가능 (포션같은경우 한슬롯에 여러개 가능)  && 또한 아이템슬롯안에 아이템 아이디가 true(존재하면)
            {
                for (int i = 1; i < items.Count; i++) // 1부터 시작함으로써 0번째 슬롯인 장비창에는 쌓이지 않는다.
                {
                    
                    if (items[i].ID == id) //이미 같은 아이디가 있는지 확인하고 있으면 그곳에 추가해주기
                    {
                        data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                        data.amount++;//인벤토리에 있는 아이템 갯수+추가 
                        player.GetComponent<PlayerControll>().potionNumber = data.amount; //데이터저장할때 필요한 포션 개수 저장
                        data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString(); // 데이터갯수만큼 text에 표시해줌
                        break;
                    }

                }
            }
            else if(!checkItemFull())//50개가 넘어가면 실행안되도록
            {
                for (int i = 1; i < items.Count; i++)
                {
                
                    if (items[i].ID == -1) // id가 -1 일때 실행 아이템이 비어있을때 실행
                    {

                        items[i] = itemToAdd;//받아온데이터를 item에 넣기
                        GameObject itemObj = Instantiate(inventoryItem);//nstantiate란 게임 내에서 오브젝트를 새로 생성하는 기능입니다. 괄호 안의 오브젝트를 생성한다는 것



                        itemObj.GetComponent<ItemData>().item = itemToAdd;
                        itemObj.GetComponent<ItemData>().slot = i;//슬롯이 몇번째 슬롯인지 확인 
                        itemObj.GetComponent<ItemData>().amount = 1;
                        itemObj.transform.SetParent(slots[i].transform);//부모를 지정해준다음 위치가 정확히 장착 위치에 오도록 지역위치를 slot[i]의 위치에 정확히 오도록 아이템 위치를 설정해줌 

                        itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite;//아이템 이미지를 불러오기 위함
                        itemObj.transform.localPosition = Vector2.zero;//아이템 데이터 위치의 로컬 포지션 을 0 로 만듦으로서 그자리에 바로 들어가게만들기위함
                        itemObj.name = itemToAdd.Title;
                        itemObj.transform.localScale = Vector3.one;
                      
                        break;

                }
                    
                     
                }
            }
            else if(checkItemFull())
            {
                player.GetComponent<PlayerControll>().alarmText("인벤토리가 가득 찼습니다");
            }

       
        
        
    }
    public bool checkItemFull()
    {
        for (int i =0; i<50;i++)
        {
            if(items[i].ID==-1)
            {
                return false;
            }
        }
        return true;
    }



    public void Addgold(int amount)
    {
        playerGold += amount;
        goldData = "G : " + playerGold;
        GoldText.GetComponent<Text>().text = goldData;
        player.GetComponent<PlayerControll>().playerGold = playerGold;
        

    }

    bool CheckIfItemIsInInventory(itemClass item)//아이템이 인벤토리안에 있는지 확인함수
    {
        for (int i = 1; i < items.Count; i++) //아이템 갯수만큼확인
        {
            if(items[i].ID==item.ID)
            {
                return true;
            }
            
        }
        return false;

      
    }
    
    bool CheckIfWeaponEquip()
    {
        if (items[0].ID == -1) //ID 가 -1 이면 비어있음
        {
            return false;
        }
        else
        {
            return true;
        }
    }





}
