using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;
public class SkillData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
{
    public SkillClass skill;
    public int slot;
    
    

    private Vector2 offset;
    SkillInterFace skInterScript;
    GameObject myEventSystem;

    //쿨타임을 만들어주기위함
    Image skillFillter;
    Text coolTimeCounterText;
    public float coolTime;
    public float currentCoolTime;
    bool canUseSkill;

    private Animator avatar;

    PlayerControll Player;

    private TooltipSkillScript tooltip;

    void Start()
    {
        avatar = GameObject.Find("Player").GetComponent<Animator>();
        skInterScript = GameObject.Find("SkillObject").GetComponent<SkillInterFace>();
        
        Player = GameObject.Find("Player").GetComponent<PlayerControll>();

        tooltip = GameObject.Find("SkillObject").GetComponent<TooltipSkillScript>();

        skillFillter = this.transform.FindChild("CoolTimeImage").GetComponentInChildren<Image>();
        coolTimeCounterText = this.GetComponentInChildren<Text>();

        
    
        coolTime = skill.CoolTime;//쿨타임을 넣어줌 받아온 데이터의
        canUseSkill = true;
        if (currentCoolTime == 0)
        {
            coolTimeCounterText.text = "";
        }
        skillFillter.fillAmount = 0; // 처음시작할댄 fill 0으로 해둠

    }


    public void useSkill()
    {
        /*
        for (int i = 0; i < 5; i++)
        {
            if (skInterScript.Skills[i].ID == skill.ID)
            {
                skInterScript.skillObj[i].GetComponent<Button>().onClick.Invoke();

            }
        }*/
        
        if (canUseSkill && Player.currentMp > skill.RequireMp)/*&&(avatar.GetCurrentAnimatorStateInfo(0).nameHash==Animator.StringToHash("Base Layer.idle")
            || avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.standing_idle")|| avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.walkneutral")||
            avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.runneutral")|| avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.standing_melee_attack_downward")
            || avatar.GetCurrentAnimatorStateInfo(0).nameHash == Animator.StringToHash("Base Layer.standing_disarm_over_shoulder"))) // 스킬이 가능하면*/
        {
             Player.skills[skill.ID] = true;
            Player.currentMp -= skill.RequireMp;
                skillFillter.fillAmount = 1;//스킬사용시작 fill을채움
                StartCoroutine("Cooltime");
                currentCoolTime = coolTime;
                coolTimeCounterText.text = "" + currentCoolTime;
                StartCoroutine("CoolTimeCounter");


                canUseSkill = false;
           
            
        }
        else
        {
            if(Player.currentMp < skill.RequireMp)
            {
                Player.GetComponent<PlayerControll>().alarmText("기력이 부족합니다");
            }
            else if(!canUseSkill)
            {
                Player.GetComponent<PlayerControll>().alarmText("쿨입니다");
            }
            coolTimeCounterText.text = "";
           
          
        }
    }

    public void onSkill()
    {
        if (canUseSkill&& Player.currentMp > skill.RequireMp)
        {
           
        }
    }

    


    IEnumerator Cooltime()
    {
            while (skillFillter.fillAmount > 0) //필터값이 0보다클떄
            {
                skillFillter.fillAmount -= 1 * Time.smoothDeltaTime / coolTime;
                yield return null;
            }
            canUseSkill = true; //스킬 쿨타임이 끝나면 스킬을 사용할 수 있는 상태로 바꿈
        

        yield break;
    }


    //남은 쿨타임을 계산할 코루틴
    IEnumerator CoolTimeCounter()
    {
        while (currentCoolTime > 0)
        {
            yield return new WaitForSeconds(1.0f);

            currentCoolTime -= 1.0f;
            coolTimeCounterText.text = "" + currentCoolTime;
            if (currentCoolTime == 0)
            {
                coolTimeCounterText.text = "";
            }
        }
        yield break;
    }







    public void OnBeginDrag(PointerEventData eventData)
    {
        
        if (skill != null && skInterScript.skillObj[slot].GetComponent<Button>().interactable == true)//아이템이 존재할때
        {
            this.transform.SetParent(this.transform.parent.parent);//슬롯패널의 위치를 가져옴 
            this.transform.position = eventData.position;
         
            GetComponent<CanvasGroup>().blocksRaycasts = false; //광선 무시를 false시킴으로써 마우스클릭한 부분을 선택할수 있도록함
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
      
        if (skill != null && skInterScript.skillObj[slot].GetComponent<Button>().interactable == true)//아이템이 존재할때
        {
            this.transform.position = eventData.position - offset;
           
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (skill != null && skInterScript.skillObj[slot].GetComponent<Button>().interactable == true)//아이템이 존재할때
        {
            this.transform.SetParent(skInterScript.sSlot[slot].transform);//움직이면 슬롯패널자식으로 사라지는것을 복귀시켜주기위함
            this.transform.position = new Vector3(skInterScript.sSlot[slot].transform.position.x + 30, skInterScript.sSlot[slot].transform.position.y, skInterScript.sSlot[slot].transform.position.z);
            GetComponent<CanvasGroup>().blocksRaycasts = true;
            this.transform.SetAsFirstSibling();
        }
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (skill != null)
            {
                offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);//아이템이 존재할때 오프셋 저장 (현재 들어온 이벤트데이터 포지션에서 
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerDrag.activeSelf)
        {
            myEventSystem = GameObject.Find("EventSystem");
            myEventSystem.GetComponent<EventSystem>().SetSelectedGameObject(null); // 이거슨.. 버튼셀렉된상태를 해제해주기위함..
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (skInterScript.skillObj[slot].GetComponent<Button>().interactable == true)
        {
            tooltip.ActivateSkill(skill);//tooltip스크립트에 선언해둔 함수 실행
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
