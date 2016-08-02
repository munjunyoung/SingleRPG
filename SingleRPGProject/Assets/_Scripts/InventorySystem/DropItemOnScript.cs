using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;//LIST를 사용하기위함

public class DropItemOnScript : MonoBehaviour {
    public itemClass dropitemData;
    private string data;//데이터를 받아올 변수
    public GameObject dropitemImage;
    public GameObject dropGoldImage;
   
    public List<GameObject> insItemObj = new List<GameObject>();//생성되는 드랍아이템오브젝트저장 (이후에 얻거나하면 지우기위해서)
    public List<GameObject> insDropUI = new List<GameObject>();//생성될 드랍아이템 ui이미지텍스트 오브젝트들의 리스트
    public List<GameObject> insDropGoldUI = new List<GameObject>();
    public List<itemClass> dropitemsData = new List<itemClass>();

    public GameObject dropGoldPrefab;
    public List<GameObject> insGoldObj = new List<GameObject>();
    public List<int> dropGoldData = new List<int>();
  





    public int ItemNumber = 0;//리스트 변수;
    public int Goldnumber = 0;

    GameObject canvas;
    ItemDatabase database;
    Vector3 dropPosition;
    Vector3 dropGoldPosition;
    public GameObject Camera;
   
    Vector3 offset;


    public int remainItem=0;
    public int remainGold = 0;
   // public GameObject SaveUI;
    //itemClass item;

    // Use this for initialization
    void Start()
    {

        //우선 아이템은 false시켜둠
        dropGoldPrefab = Resources.Load<GameObject>("DropItem/GoldObject");
        database = GameObject.Find("InventoryObject").GetComponent<ItemDatabase>();
        
        canvas = GameObject.Find("DropImage");
        canvas.SetActive(false);
    }

    void Update()
    {

        //유아이 위치는 항상업데이트되야 그자리에 잇어보임..
        if (canvas.activeSelf)
        {
            for (int t = 0; t < Goldnumber; t++)
            {
                if (insDropGoldUI[t] != null)
                {
                    insDropGoldUI[t].transform.position = Camera.GetComponent<Camera>().WorldToScreenPoint(new Vector3(insGoldObj[t].transform.position.x, insGoldObj[t].transform.position.y, insGoldObj[t].transform.position.z));//)new Vector3(insItemObj[j].transform.position.x, insItemObj[j].transform.position.y - 2.0f, insItemObj[j].transform.position.z));

                }
            }
            for (int j = 0; j < ItemNumber; j++)
            {
                if (insDropUI[j] != null)
                {
                    insDropUI[j].transform.position = Camera.GetComponent<Camera>().WorldToScreenPoint(new Vector3(insItemObj[j].transform.position.x, insItemObj[j].transform.position.y, insItemObj[j].transform.position.z));//)new Vector3(insItemObj[j].transform.position.x, insItemObj[j].transform.position.y - 2.0f, insItemObj[j].transform.position.z));

                }
            }

           
        }

        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            Activate();
        }
        else if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Deactivate();
        }

   
    }
    public void Activate()
    {
        canvas.SetActive(true);

    }

    public void Deactivate()
    {
        canvas.SetActive(false);
    }

  

    public void DropGold(int amount, Vector3 position)//코인드랍
    {
        insGoldObj.Add(Instantiate(dropGoldPrefab));
        insGoldObj[Goldnumber].transform.position = new Vector3(position.x, position.y+1, position.z);
        insGoldObj[Goldnumber].transform.localRotation = Quaternion.Euler(0, 0, 0);
        insGoldObj[Goldnumber].transform.localScale = Vector3.one;

        dropGoldData.Add(amount);//골드양넣기


      //  dropGoldPosition = insGoldObj[Goldnumber].transform.position; //유아이 위치 포지션 설정
       // dropGoldPosition = new Vector3(dropGoldPosition.x, dropGoldPosition.y - 2.5f, dropGoldPosition.z);

        insDropGoldUI.Add(Instantiate(dropGoldImage));

       

       
        insDropGoldUI[Goldnumber].transform.SetParent(canvas.transform);
        insDropGoldUI[Goldnumber].transform.localScale = Vector3.one;

        insDropGoldUI[Goldnumber].transform.localRotation = Quaternion.Euler(0, 0, 0);
        insDropGoldUI[Goldnumber].transform.position = Camera.GetComponent<Camera>().WorldToScreenPoint(dropGoldPosition);

       
        data = "<color=#ffffff><b>" + dropGoldData[Goldnumber] + " Gold</b></color>\n\n ";//타이틀
        

        insDropGoldUI[Goldnumber].GetComponent<DropGoldGetScript>().dropGoldId = Goldnumber;
        insDropGoldUI[Goldnumber].GetComponent<DropGoldGetScript>().Goldamount = amount;
        insDropGoldUI[Goldnumber].transform.GetChild(0).GetComponent<Text>().text = data; //바로 텍스트 넣어두기

        ////드랍한 아이템 포지션

        insDropGoldUI[Goldnumber].SetActive(true);

        

        Goldnumber++;
        remainGold++;
        
    }



    public void DropItem(int dropitemID, Vector3 position) //몬스터 드랍아이템 //드랍할위치(적이 죽은포지션), 드랍할 아이템 id)
    {
        
        itemClass itemToAdd = database.FetchItemByID(dropitemID);
        InventoryScript EquipWeaponData; //아이템 정보
        EquipWeaponData = GameObject.Find("InventoryObject").GetComponent<InventoryScript>(); //아이템 데이터정보를 받아오기위한 스크립

        GameObject dropitemPrefab;//prefab을 가져올 오브젝트변수
        dropitemPrefab = Resources.Load<GameObject>("DropItem/D" + itemToAdd.Slug) as GameObject; // 아이템 데이터 정보
        position = new Vector3(position.x, position.y + 2, position.z);//받아오는 포지션은 죽은에너미의 위치포지션이므로 그것보다 조금더 높은높이에서 아이템이떨어지게 하기위함
        

        insItemObj.Add(Instantiate(dropitemPrefab));//드랍아이템을 리스트에 추가
        insItemObj[ItemNumber].transform.position = position;//생성된 오브젝트의 포지션설정
        insItemObj[ItemNumber].transform.localScale = Vector3.one*5;//사이즈가 작게나와서 그냥설정해줫음

        
        dropPosition = insItemObj[ItemNumber].transform.position; //유아이 위치 포지션 설정
        dropPosition = new Vector3(dropPosition.x, dropPosition.y - 2.5f, dropPosition.z);//원래 아이템 위치보다 조금 낮게 설정하기위함

        //id 가 0 인 아이템 데이터 정보를 가져옴

        dropitemsData.Add(new itemClass());//리스트 자리확보
        dropitemsData[ItemNumber] = itemToAdd; //받아온 아이템 데이터들 저장

        
    
        insDropUI.Add(Instantiate(dropitemImage));

        // dropitemImage.SetActive(false);

        insDropUI[ItemNumber].transform.SetParent(canvas.transform);
        insDropUI[ItemNumber].transform.localScale = Vector3.one;
      
        insDropUI[ItemNumber].transform.localRotation = Quaternion.Euler(0, 0, 0);
        insDropUI[ItemNumber].transform.position = Camera.GetComponent<Camera>().WorldToScreenPoint(dropPosition);

        if (dropitemsData[ItemNumber].Value >= 1000)
        {
            data = "<color=#CC00CC><b>" + dropitemsData[ItemNumber].Title + "</b></color>\n\n ";
        }
        if (dropitemsData[ItemNumber].Value<1000&&dropitemsData[ItemNumber].Value >= 700)
        {
            data = "<color=#0f73f0><b>" + dropitemsData[ItemNumber].Title + "</b></color>\n\n ";//타이틀
        }
        else if(dropitemsData[ItemNumber].Value<700&& dropitemsData[ItemNumber].Value >=500)
        {
            data = "<color=#33CC33><b>" + dropitemsData[ItemNumber].Title + "</b></color>\n\n ";//타이틀
        }
        else if(dropitemsData[ItemNumber].Value < 500)
        {
            data = "<color=#ffffff><b>" + dropitemsData[ItemNumber].Title + "</b></color>\n\n ";//타이틀
        }

        
   

        insDropUI[ItemNumber].GetComponent<DropItemGetScript>().dropitemId = ItemNumber;
        insDropUI[ItemNumber].transform.GetChild(0).GetComponent<Text>().text = data; //바로 텍스트 넣어두기

        ////드랍한 아이템 포지션

        insDropUI[ItemNumber].SetActive(true);

        ItemNumber++;//리스트의 i숫자
        remainItem++;//남아있는 아이템숫자확인
       // i++; //아이템을 한번 드랍할때마다 i 숫자 올림

    }

    public void DropObjClear()
    {
        for (int i = 0; i < insDropGoldUI.Count; i++)//젠될때 슬라이더는 canvas에 존재하므로 삭제되지않음 그러므로 확인하고 삭제함
        {
            if (insDropGoldUI[i] != null)
            {
                Destroy(insDropGoldUI[i]);
            }
        }
        for (int i = 0; i < insDropUI.Count; i++)//젠될때 슬라이더는 canvas에 존재하므로 삭제되지않음 그러므로 확인하고 삭제함
        {
            if (insDropUI[i] != null)
            {
                Destroy(insDropUI[i]);
            }
        }

        insItemObj.Clear();

        insDropUI.Clear();
        insDropGoldUI.Clear();
        dropitemsData.Clear();


        insGoldObj.Clear();
        dropGoldData.Clear();

        Goldnumber = 0;
        ItemNumber = 0;
        remainGold = 0;
        remainItem = 0;
    }
}
