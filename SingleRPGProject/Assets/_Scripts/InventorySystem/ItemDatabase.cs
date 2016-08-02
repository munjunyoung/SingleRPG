using UnityEngine;
using System.Collections;
using LitJson;//JSON 사용
using System.Collections.Generic;//https://msdn.microsoft.com/ko-kr/library/windows/apps/xaml/system.collections.generic(v=vs.110).aspx
using System.IO;//시스템 인풋아웃풋

public class ItemDatabase : MonoBehaviour
{
    private List<itemClass> database = new List<itemClass>();
    private JsonData itemData;

    void Awake()
    {
        //추가
        //itemClass item = new itemClass(0,"Ball", 5);
        //database.Add(item);
        //Debug.Log(database[0].Title);
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/items.json"));//파싱  "" 읽어오는 파일경로 설정하여 jsondata형인 itemdata에 모두 저장 

        ConstructItemDatabase();
       
       
    }


    public itemClass FetchItemByID(int id)// 아이템 id를 통하여 그 아이템의 정보들을 가져와 리턴 
    {
        for(int i = 0; i< database.Count; i++)
        {
            if (database[i].ID == id)
            {
                return database[i];
            }
        }
        return null;
    }

    void ConstructItemDatabase() //데이터베이스 생성
    {
        for (int i = 0; i < itemData.Count; i++) // 아이템 데이터수만큼 리스트에 넣기
        {
            database.Add(new itemClass((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["value"], (int)itemData[i]["stats"]["power"], itemData[i]["description"].ToString(), (bool)itemData[i]["stackable"], itemData[i]["slug"].ToString(), itemData[i]["type"].ToString())); //데이터베이스 list에 받아온 제이슨데이터를 모두 넣기 (오류가 나기떄문에 모드 cast해줘야함) 변수형으로 변환시켜줘야함
        }


    }
}


public class itemClass // itemclass list에 들어갈 형태
{
    public int ID { get; set; }//아이템 아이디
    public string Title { get; set; } //아이템 타이틀
    public int Value { get; set; }// 값
    public int Power { get; set; }//아이템 공격력
    public string Description { get; set; }// 아이템 설명
    public bool Stackable { get; set; }
    public string Slug { get; set; }// 아이템 이미지 파일명 확인 
    public string Type { get; set; }

    public Sprite Sprite { get; set; }//아이템 이미지
    

    public itemClass(int id, string title, int value, int power, string description, bool stackable,  string slug, string type)
    {
       
        this.ID = id;
        this.Title = title;
        this.Value = value;
        this.Power = power;
        this.Description = description;
        this.Stackable = stackable;
        this.Slug = slug;
        this.Type = type;
        this.Sprite = Resources.Load<Sprite>("_InvenSprite/" + slug);
    }

    
    public itemClass() //아이템이 존재할때 -1? //아무것도없을때 -1
    {
        this.ID = -1;
    }

}

