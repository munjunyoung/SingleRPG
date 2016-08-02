using UnityEngine;
using System.Collections;
using LitJson;//JSON 사용
using System.Collections.Generic;//https://msdn.microsoft.com/ko-kr/library/windows/apps/xaml/system.collections.generic(v=vs.110).aspx
using System.IO;//시스템 인풋아웃풋

public class SkillDatabase : MonoBehaviour {
    private List<SkillClass> skilldatabaseList = new List<SkillClass>();
    private JsonData skillData;
    public int datacount;

    void Start()
    {

        skillData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/skills.json"));//파싱  "" 읽어오는 파일경로 설정하여 jsondata형인 itemdata에 모두 저장 
        ConstructSkillDatabase();
        datacount = skilldatabaseList.Count;
     
        
    }

    public SkillClass FetchSkillByID(int id) // 이함수를 불러옴으로서 데이터를 넘겨줌
    {
        for (int i = 0; i < skilldatabaseList.Count; i++)
        {
            if (skilldatabaseList[i].ID == id)
            {
                return skilldatabaseList[i];
            }
        }


        return null;
    }

    void ConstructSkillDatabase() //데이터베이스 생성
    {
        for (int i = 0; i < skillData.Count; i++) // 아이템 데이터수만큼 리스트에 넣기
        {
            skilldatabaseList.Add(new SkillClass((int)skillData[i]["id"], skillData[i]["title"].ToString(), skillData[i]["description"].ToString(), skillData[i]["slug"].ToString(), skillData[i]["type"].ToString(), (int)skillData[i]["cooltime"], (int)skillData[i]["level"], (int)skillData[i]["durationtime"], (int)skillData[i]["requiremp"])); //데이터베이스 list에 받아온 제이슨데이터를 모두 넣기 (오류가 나기떄문에 모드 cast해줘야함) 변수형으로 변환시켜줘야함
        }


    }



}


public class SkillClass
{
    public int ID { get; set; }//아이템 아이디
    public string Title { get; set; } //아이템 타이틀
    public string Description { get; set; }// 아이템 설명
    public string Slug { get; set; }// 아이템 이미지 파일명 확인 
    public string Type { get; set; }
    public int CoolTime { get; set; }
    public int Level { get; set; }
    public int Durationtime { get; set; }
    public int RequireMp { get; set; }
    public Sprite Sprite { get; set; }
    


    public SkillClass(int id,string title, string description, string slug, string type, int cooltime, int level, int durationtime, int requiremp)
    {
        this.ID = id;
        this.Title = title;
        this.Description = description;
        this.Slug = slug;
        this.Type = type;
        this.CoolTime = cooltime;
        this.Level = level;
        this.Durationtime = durationtime;
        this.RequireMp = requiremp;
        this.Sprite = Resources.Load<Sprite>("SkillIcon/" + slug);
    }

    public SkillClass() //아이템이 존재할때 -1? //아무것도없을때 -1
    {
        this.ID = -1;
    }


}
