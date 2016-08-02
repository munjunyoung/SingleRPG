using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TooltipScript : MonoBehaviour {
    private itemClass item;//아이템클래스 형변수
    private SkillClass skill;
    private string data;//데이터를 받아올 변수
    private GameObject tooltip;
    private Camera cam;

    void Start()
    {
        tooltip = GameObject.Find("TooltipImageItem");
        tooltip.SetActive(false);
        cam = GameObject.Find("UICamera").GetComponent<Camera>();
    }

    void Update()
    {
        //툴팁이 나오는 위치를 마우스 포지션위치로 하기위함
        if(tooltip.activeSelf)//툴팁이 온되있을때
        {
           
            tooltip.transform.position =Input.mousePosition; //마우스포지션으로 툴팁위치를 지정
        
        }
    }
    //아이템쪽..
    public void Activate(itemClass item)
    {
        this.item = item;
        ConstrucDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate()
    {
        tooltip.SetActive(false);
    }
	
    public void ConstrucDataString()
    {
        if (item != null)
        {
            if (item.Type == "Weapon")
            {
                if (item.Value >= 1000)
                {
                    data = " <color=#CC00CC><b>\n 이름 : " + item.Title + "\n</b></color> 종류 : " + item.Type + "\n\n " + item.Description + "\n 공격력 : " + item.Power + "\n" + " 가격 : " + item.Value + "\n";//타이틀
                }

                if (item.Value < 1000 && item.Value >= 700)
                {
                    data = " <color=#0f73f0><b>\n 이름 : " + item.Title + "\n</b></color> 종류 : " + item.Type + "\n\n " + item.Description + "\n 공격력 : " + item.Power + "\n" + " 가격 : " + item.Value + "\n";//타이틀
                }
                if (item.Value < 700 && item.Value >= 500) //가격에따라서 색깔바꿔주기
                {
                    data = " <color=#33CC33><b>\n 이름 : " + item.Title + "\n</b></color> 종류 : " + item.Type + "\n\n " + item.Description + "\n 공격력 : " + item.Power + "\n" + " 가격 : " + item.Value + "\n";//타이틀
                }
                if (item.Value < 500)
                {
                    data = " <color=#ffffff><b>\n 이름 : " + item.Title + "\n</b></color> 종류 : " + item.Type + "\n\n " + item.Description + "\n 공격력 : " + item.Power + "\n" + " 가격 : " + item.Value + "\n";//타이틀
                }
            }
            else if (item.Type == "Food")
            {
                if (item.Value < 500) //가격에따라서 색깔바꿔주기
                {
                    data = " <color=#ffffff><b>\n 이름 : " + item.Title + "\n</b></color> 종류 : " + item.Type + "\n\n " + item.Description + "\n 회복량 : " + item.Power + "\n" + " 가격 : " + item.Value + "\n";//타이틀
                }
            }

            tooltip.transform.GetChild(0).GetComponent<Text>().text = data;
        }
    }

    

}
