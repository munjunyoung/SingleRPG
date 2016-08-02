using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SkillInterFace : MonoBehaviour {

    public GameObject skillSlot;
    public GameObject skillButton;
    GameObject skillSlotPanel;
    SkillDatabase skillDB;
    GameObject skillWindowPanel;

    
   
    public List<GameObject> sSlot = new List<GameObject>();
    public List<SkillClass> Skills = new List<SkillClass>();
    public List<GameObject> skillObj = new List<GameObject>();

    int slotCount = 14;
    int skillnumber;

	// Use this for initialization
	void Start () {
        skillSlotPanel = GameObject.Find("SkillInterface Slot Panel");
        skillDB = GameObject.Find("SkillObject").GetComponent<SkillDatabase>();
       
        skillnumber = 0;
        //myEventSystem = GameObject.Find("EventSystem");

        for (int i = 0;i<slotCount-7;i++)
        {
            skillWindowPanel = GameObject.Find("Slot Panel" + i);
            Skills.Add(new SkillClass());
         
            sSlot.Add(Instantiate(skillSlot));
            sSlot[i].transform.SetParent(skillWindowPanel.transform);
            sSlot[i].transform.localPosition = Vector3.zero;
            sSlot[i].transform.localScale = Vector3.one;
            sSlot[i].transform.FindChild("Text").gameObject.SetActive(false);
            skillObj.Add(null);
            sSlot[i].GetComponent<SkillSlot>().id = i;//id설정
            //번호설정
            attachSkill(i, i);//스킬창 스킬선언
            skillObj[i].GetComponent<Button>().enabled = false;

     
        }


        for(int i=7;i<slotCount;i++) //56789 는 하단 스킬슬롯
        {
            Skills.Add(new SkillClass());
            
            sSlot.Add(Instantiate(skillSlot));
            sSlot[i].transform.SetParent(skillSlotPanel.transform);
            sSlot[i].transform.localPosition = Vector3.zero;
            sSlot[i].transform.localScale = Vector3.one;
            sSlot[i].GetComponent<SkillSlot>().id = i;//id설정
            sSlot[i].GetComponentInChildren<Text>().text = (i-6).ToString(); //번호설정
            skillObj.Add(null);

        }
       

    }

    void Update()
    {
        UserinterfaceKeybutton();
       
    }


    public void UserinterfaceKeybutton()
    {
        
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (skillObj[7]!=null)
            {
                skillObj[7].GetComponent<Button>().onClick.Invoke();
                
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (skillObj[8] != null)
            {
                
                skillObj[8].GetComponent<Button>().onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (skillObj[9] != null)
            {
                skillObj[9].GetComponent<Button>().onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            if (skillObj[10] != null)
            {
                skillObj[10].GetComponent<Button>().onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (skillObj[11] != null)
            {
                skillObj[11].GetComponent<Button>().onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            if (skillObj[12] != null)
            {
                skillObj[12].GetComponent<Button>().onClick.Invoke();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            if (skillObj[13] != null)
            {
                skillObj[13].GetComponent<Button>().onClick.Invoke();
            }
        }

    }


    public void attachSkill(int SkillID, int slotID)
    {
        SkillClass skillToAdd = skillDB.FetchSkillByID(SkillID);//여기서의 id는 내가 드래그할 스킬의 id를 가져와야함  
        

        Skills[slotID] = skillToAdd; //슬롯과 같은 순서에 SKILL 정보를 넣어줌
        skillObj[slotID] = Instantiate(skillButton);
        skillObj[slotID].transform.SetParent(sSlot[slotID].transform);
        skillObj[slotID].GetComponent<SkillData>().skill = skillToAdd;
       
        skillObj[slotID].transform.localPosition = new Vector3(30, 0, 0); //왜이래..
        skillObj[slotID].transform.localScale = Vector3.one;


        skillObj[slotID].GetComponent<SkillData>().slot = slotID;

        

        skillObj[slotID].GetComponent<Image>().sprite = skillToAdd.Sprite; //스프라이트 넣어주공


        //생성된 버튼 리스너 추가
    
          //  skillObj[slotID].GetComponent<Button>().onClick.AddListener(() => skillObj[slotID].GetComponent<SkillData>().onSkill());
         //   skillObj[slotID].GetComponent<Button>().onClick.AddListener(() => skillObj[slotID].GetComponent<SkillData>().useSkill());

    }
}
