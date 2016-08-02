using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TooltipSkillScript : MonoBehaviour {
    
    private SkillClass skill;
    private string data;//데이터를 받아올 변수
    private GameObject tooltip;

    void Start()
    {
        tooltip = GameObject.Find("TooltipImageSkill");
        tooltip.SetActive(false);
    }

    void Update()
    {
        //툴팁이 나오는 위치를 마우스 포지션위치로 하기위함
        if (tooltip.activeSelf)//툴팁이 온되있을때
        {
            tooltip.transform.position = Input.mousePosition; //마우스포지션으로 툴팁위치를 지정
        }
    }
 

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }



    //스킬도 합시다..

    public void ActivateSkill(SkillClass skill)
    {
        this.skill = skill;
        ConstrucDataStringSkill();
        tooltip.SetActive(true);
    }


    public void ConstrucDataStringSkill()
    {
        if (skill.Type == "Active")
        {
            data = " <color=#0f73f0><b>\n 스킬이름 : " + skill.Title + "\n</b></color>\n 스킬타입 : " + skill.Type + "\n\n 스킬설명 : " + skill.Description + "\n 기력 : " + skill.RequireMp + "\n" + " 지속시간 : " + skill.Durationtime + "\n" + " 쿨타임 : " + skill.CoolTime + "\n";//타이틀
        }
        else if(skill.Type=="Buff")
        {
            data = " <color=#FF9900><b>\n 스킬이름 : " + skill.Title + "\n</b></color>\n 스킬타입 : " + skill.Type + "\n\n 스킬설명 : " + skill.Description + "\n 기력 : " + skill.RequireMp + "\n" + " 지속시간 : " + skill.Durationtime + "\n" + " 쿨타임 : " + skill.CoolTime + "\n";//타이틀
        }
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
    }

}
