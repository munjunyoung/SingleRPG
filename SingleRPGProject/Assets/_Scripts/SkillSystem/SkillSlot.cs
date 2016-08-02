using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour, IDropHandler
{
    public int id;
    SkillInterFace skillScript;

    // Use this for initialization
    void Start()
    {
        skillScript = GameObject.Find("SkillObject").GetComponent<SkillInterFace>();
    }

   public void OnDrop(PointerEventData eventData)
    {
        SkillData dropSkill = eventData.pointerDrag.GetComponent<SkillData>();
        if (skillScript.skillObj[dropSkill.slot].GetComponent<Button>().interactable == true)
        {
            if (dropSkill.slot < 7) //내가 집은 스킬의 slot이 스킬창에서 꺼내오는 경우
            {
                if (skillScript.Skills[id].ID == -1)//비어있을경우
                {

                    skillScript.attachSkill(dropSkill.skill.ID, id);
                    skillScript.Skills[id] = dropSkill.skill;
                    skillScript.skillObj[id].transform.SetAsFirstSibling();

                    for (int i = 7; i < 14; i++)
                    {


                        if (skillScript.Skills[id].ID == skillScript.Skills[i].ID && id != i)//슬롯에 같은아이디가 있으면 지워버리자 //해당스킬슬롯빼고
                        {
                            Destroy(skillScript.skillObj[i]);
                            skillScript.skillObj[i] = null;
                            skillScript.Skills[i] = new SkillClass();
                        }
                    }


                }
                else //존재하면
                {
                    Destroy(skillScript.skillObj[id]);

                    skillScript.attachSkill(dropSkill.skill.ID, id);
                    // dropSkill.slot = id;
                    // dropSkill.transform.SetParent(this.transform);
                    // dropSkill.transform.position = this.transform.position;
                    skillScript.skillObj[id].transform.SetAsFirstSibling();
                    skillScript.Skills[id] = dropSkill.skill;
                    skillScript.skillObj[id].transform.SetAsFirstSibling();

                    for (int i = 7; i < 14; i++)
                    {
                        if (skillScript.Skills[id].ID == skillScript.Skills[i].ID && id != i)//슬롯에 같은아이디가 있으면 지워버리자 //해당스킬슬롯빼고
                        {
                            Destroy(skillScript.skillObj[i]);
                            skillScript.skillObj[i] = null;
                            skillScript.Skills[i] = new SkillClass();
                        }
                    }

                }
            }
            else if (dropSkill.slot >= 5)
            {
                if (skillScript.Skills[id].ID == -1)//비어있을경우
                {
                    //드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
                    //   skillScript.attachSkill(dropSkill.skill.ID, id);
                    //  skillScript.Skills[id] = dropSkill.skill;


                    skillScript.Skills[id] = dropSkill.skill;
                    skillScript.skillObj[id] = skillScript.skillObj[dropSkill.slot];


                    skillScript.skillObj[dropSkill.slot] = null;
                    skillScript.Skills[dropSkill.slot] = new SkillClass();
                    // Destroy(dropSkill.gameObject);
                    skillScript.skillObj[id].transform.localScale = new Vector3(0.87f, 0.87f, 0);
                    dropSkill.slot = id;//slot을 이동한 슬롯 id로 변경


                    /*    for (int i = 0; i < 10; i++)
                        {
                            Debug.Log(skillScript.Skills[i].ID);
                            Debug.Log(skillScript.skillObj[i]);
                            Debug.Log(skillScript.sSlot[i].GetComponent<SkillSlot>().id);
                        }*/


                }
                else //존재하면
                {
                    if (dropSkill.slot != id) //똑같은자리에서 움직이지 않을경우
                    {
                        Destroy(skillScript.skillObj[id]);
                        //     skillScript.skillObj[id] = null;
                        //     skillScript.Skills[id] = new SkillClass();

                        skillScript.Skills[dropSkill.slot] = new SkillClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성 
                        skillScript.Skills[id] = dropSkill.skill;

                        skillScript.skillObj[id] = skillScript.skillObj[dropSkill.slot];

                        skillScript.skillObj[dropSkill.slot] = null;
                        skillScript.Skills[dropSkill.slot] = new SkillClass();
                        dropSkill.slot = id;//slot을 이동한 슬롯 id로 변경

                        skillScript.skillObj[id].transform.localScale = new Vector3(0.87f, 0.87f, 0);

                    }
                    else
                    {
                        skillScript.skillObj[id].transform.localScale = new Vector3(0.87f, 0.87f, 0);

                    }

                }




            }
        }

    }



}
