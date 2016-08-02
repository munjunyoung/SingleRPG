using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class SkillWindowControlScript : MonoBehaviour {
    public bool Skill1RockPossibleCheck;
    public bool Skill2RockPossibleCheck;
    public bool Skill3RockPossibleCheck;
    public bool Skill4RockPossibleCheck;
    public bool Skill5RockPossibleCheck;
    public bool Skill6RockPossibleCheck;
    public bool Skill7RockPossibleCheck;


    GameObject Player;

    GameObject SkillTextButton1;
    GameObject SkillImageButton1;

    GameObject SkillTextButton2;
    GameObject SkillImageButton2;

    GameObject SkillTextButton3;
    GameObject SkillImageButton3;

    GameObject SkillTextButton4;
    GameObject SkillImageButton4;

    GameObject SkillTextButton5;
    GameObject SkillImageButton5;

    GameObject SkillTextButton6;

    GameObject SkillTextButton7;

    SkillInterFace skInterfaceSC;

    public List<bool> skillrockList = new List<bool>();
    public List<GameObject> SkillTextList = new List<GameObject>();


    void Awake()
    {

        skInterfaceSC = GameObject.Find("SkillObject").GetComponent<SkillInterFace>();
        Player = GameObject.Find("Player");

    }
    // Use this for initialization
    void Start() {
        Skill1RockPossibleCheck = true;
        Skill2RockPossibleCheck = true;
        Skill3RockPossibleCheck = true;
        Skill4RockPossibleCheck = true;
        Skill5RockPossibleCheck = true;
        Skill6RockPossibleCheck = true;
        Skill7RockPossibleCheck = true;


        for(int i=0;i<7;i++)
        {
            skInterfaceSC.skillObj[i].GetComponent<Button>().interactable = false;
            skInterfaceSC.skillObj[i].GetComponent<Image>().color = new Color(0.3f, 0.3f, 0.3f, 1f); //어두워보이게
        }
        /*
       
        skInterfaceSC.skillObj[1].GetComponent<Button>().interactable = false;
        skInterfaceSC.skillObj[2].GetComponent<Button>().interactable = false;
        skInterfaceSC.skillObj[3].GetComponent<Button>().interactable = false;
        skInterfaceSC.skillObj[4].GetComponent<Button>().interactable = false;
        skInterfaceSC.skillObj[5].GetComponent<Button>().interactable = false;
        skInterfaceSC.skillObj[6].GetComponent<Button>().interactable = false;
        */





        SkillTextButton1 = GameObject.Find("SkillTextButton1");
        SkillTextButton2 = GameObject.Find("SkillTextButton2");
        SkillTextButton3 = GameObject.Find("SkillTextButton3");
        SkillTextButton4 = GameObject.Find("SkillTextButton4");
        SkillTextButton5 = GameObject.Find("SkillTextButton5");
        SkillTextButton6 = GameObject.Find("SkillTextButton6");
        SkillTextButton7 = GameObject.Find("SkillTextButton7");


        //락 bool리스트 모두 초기화
        for (int i = 0; i < 7; i++)
        {
            skillrockList.Add(false);
        }


        SkillTextList.Add(SkillTextButton1);
        SkillTextList.Add(SkillTextButton2);
        SkillTextList.Add(SkillTextButton3);
        SkillTextList.Add(SkillTextButton4);
        SkillTextList.Add(SkillTextButton5);
        SkillTextList.Add(SkillTextButton6);
        SkillTextList.Add(SkillTextButton7);




        //skill window 셋팅
        for (int i = 0; i < 7; i++)
        {
            //SkillTextList[i].GetComponent<Image>().color = new Color(120/255f, 120 / 255f, 120 / 255f, 1f); 

            SkillTextList[i].GetComponentInChildren<Text>().fontSize = 20;
            SkillTextList[i].GetComponentInChildren<Text>().text = "ROCK    level - " + skInterfaceSC.Skills[i].Level;
            SkillTextList[i].GetComponentInChildren<Text>().color = new Color(167 / 255f,0,0,1);
            SkillTextList[i].GetComponent<Button>().interactable = false;

        }



        SkillTextList[0].GetComponent<Button>().onClick.AddListener(() => onRockSkill1());
        SkillTextList[1].GetComponent<Button>().onClick.AddListener(() => onRockSkill2());
        SkillTextList[2].GetComponent<Button>().onClick.AddListener(() => onRockSkill3());
        SkillTextList[3].GetComponent<Button>().onClick.AddListener(() => onRockSkill4());
        SkillTextList[4].GetComponent<Button>().onClick.AddListener(() => onRockSkill5());
        SkillTextList[5].GetComponent<Button>().onClick.AddListener(() => onRockSkill6());
        SkillTextList[6].GetComponent<Button>().onClick.AddListener(() => onRockSkill7());



       RockOFFSkill((int)Player.GetComponent<PlayerControll>().PlayerLevel);//awake로 실행되서 함수가실행할떄 skintersc 스킬들이 null pointer뜸 처음시작은 이걸로
        //저장한데이터를 로드할때 이미 오픈시킨 스킬창을 오픈시켜두기위함;
        if (Player.GetComponent<PlayerControll>().wSkillOn[0] == true)
        {
            onRockSkill1();
        }
        if (Player.GetComponent<PlayerControll>().wSkillOn[1] == true)
        {
            onRockSkill2();
        }
        if (Player.GetComponent<PlayerControll>().wSkillOn[2] == true)
        {
            onRockSkill3();
        }
        if (Player.GetComponent<PlayerControll>().wSkillOn[3] == true)
        {
            onRockSkill4();
        }
        if (Player.GetComponent<PlayerControll>().wSkillOn[4] == true)
        {
            onRockSkill5();
        }
        if (Player.GetComponent<PlayerControll>().wSkillOn[5] == true)
        {
            onRockSkill6();
        }
        if (Player.GetComponent<PlayerControll>().wSkillOn[6] == true)
        {
            onRockSkill7();
        }

    }
	
    /// <summary>
    /// ///////////////////////////////
    /// </summary>
    /// <param name="playerLV"></param>

        //레벨이 오르면 스킬도금이 벗겨져서 스킬을 열수 있도록함
    public void RockOFFSkill(int playerLV)
    {
        for(int i=0;i<7;i++)
        {
            if(skInterfaceSC.Skills[i].Level<=playerLV && skillrockList[i]==false) //스킬잠금레벨이 플레이어 레벨보다 낮고, 해당하는 스킬락이 풀려있지 않을경우
            {
                skillrockList[i] = true;
                SkillTextList[i].GetComponentInChildren<Text>().text = "Possible";
                SkillTextList[i].GetComponentInChildren<Text>().color = new Color(25 / 255f, 175 / 255f, 20 / 255f, 1f);
                SkillTextList[i].GetComponent<Button>().interactable = true;
                


            }
        }
        
    }

    //처음 스킬을시작할대 배우지않았으면 text만 선택가능하게하고 락이 풀렸을시에 text클릭은 닫게되고 image클릭이 가능하다
    public void onRockSkill1() // 스킬락을 풀기
    {
        if (skillrockList[0])//스킬락을 풀수있는지 확인 레벨확인
        {
         
            SkillTextButton1.GetComponent<Button>().enabled = false;
            SkillTextButton1.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[0].Title;
            SkillTextButton1.GetComponentInChildren<Text>().fontSize = 15;
            SkillTextButton1.GetComponentInChildren<Text>().color = Color.white;
            skInterfaceSC.skillObj[0].GetComponent<Button>().interactable = true;
            skInterfaceSC.skillObj[0].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[0] = true;

        }
        
       
    }
    public void onRockSkill2()
    {
        if (skillrockList[1])//스킬락을 풀수있는지 확인 레벨확인
        {
            SkillTextButton2.GetComponent<Button>().enabled = false;
            SkillTextButton2.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[1].Title;
            SkillTextButton2.GetComponentInChildren<Text>().color = Color.white;
            SkillTextButton2.GetComponentInChildren<Text>().fontSize = 15;

            skInterfaceSC.skillObj[1].GetComponent<Button>().interactable = true;
            skInterfaceSC.skillObj[1].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[1] = true;

        }
        else
        {
            //유아이가 나오게하든 얘기하기
        }
    }
    public void onRockSkill3()
    {
        if (skillrockList[2])//스킬락을 풀수있는지 확인 레벨확인
        {
            SkillTextButton3.GetComponent<Button>().enabled = false;
            SkillTextButton3.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[2].Title;
            SkillTextButton3.GetComponentInChildren<Text>().color = Color.white;
            SkillTextButton3.GetComponentInChildren<Text>().fontSize = 15;

            skInterfaceSC.skillObj[2].GetComponent<Button>().interactable = true;
            skInterfaceSC.skillObj[2].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[2] = true;

        }
        else
        {
            //유아이가 나오게하든 얘기하기
        }
    }
    public void onRockSkill4()
    {
        if (skillrockList[3])//스킬락을 풀수있는지 확인 레벨확인
        {
            SkillTextButton4.GetComponent<Button>().enabled = false;
            SkillTextButton4.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[3].Title;
            SkillTextButton4.GetComponentInChildren<Text>().fontSize = 15;
            skInterfaceSC.skillObj[3].GetComponent<Button>().interactable = true;
            SkillTextButton4.GetComponentInChildren<Text>().color = Color.white;

            skInterfaceSC.skillObj[3].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[3] = true;
        }
        else
        {
            //유아이가 나오게하든 얘기하기
        }
    }
    public void onRockSkill5()
    {
        if (skillrockList[4])//스킬락을 풀수있는지 확인 레벨확인
        {
            SkillTextButton5.GetComponent<Button>().enabled = false;
            SkillTextButton5.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[4].Title;
            SkillTextButton5.GetComponentInChildren<Text>().fontSize = 15;
            skInterfaceSC.skillObj[4].GetComponent<Button>().interactable = true;
            SkillTextButton5.GetComponentInChildren<Text>().color = Color.white;

            skInterfaceSC.skillObj[4].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[4] = true;
        }
        else
        {
            //유아이가 나오게하든 얘기하기
        }
    }

    public void onRockSkill6()
    {
        if (skillrockList[5])//스킬락을 풀수있는지 확인 레벨확인
        {
            SkillTextButton6.GetComponent<Button>().enabled = false;
            SkillTextButton6.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[5].Title;
            SkillTextButton6.GetComponentInChildren<Text>().fontSize = 15;
            skInterfaceSC.skillObj[5].GetComponent<Button>().interactable = true;
            SkillTextButton6.GetComponentInChildren<Text>().color = Color.white;

            skInterfaceSC.skillObj[5].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[5] = true;
        }
        else
        {
            //유아이가 나오게하든 얘기하기
        }
    }


    public void onRockSkill7()
    {
        if (skillrockList[6])//스킬락을 풀수있는지 확인 레벨확인
        {
            SkillTextButton7.GetComponent<Button>().enabled = false;
            SkillTextButton7.GetComponentInChildren<Text>().text = skInterfaceSC.Skills[6].Title;
            skInterfaceSC.skillObj[6].GetComponent<Button>().interactable = true;
            SkillTextButton7.GetComponentInChildren<Text>().fontSize = 15;
            SkillTextButton7.GetComponentInChildren<Text>().color = Color.white;

            skInterfaceSC.skillObj[6].GetComponent<Image>().color = Color.white;
            Player.GetComponent<PlayerControll>().wSkillOn[6] = true;
        }
        else
        {
            //유아이가 나오게하든 얘기하기
        }
    }

    
}
