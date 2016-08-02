using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using UnityEngine.UI;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler {
    public itemClass item;
    public int amount; //
    public int slot;
    GameObject abilityObject;//캐릭터 패널에서 공격력 반영하기위함


    private Vector2 offset;

    private InventoryScript inv;

    private TooltipScript tooltip;//툴팁스크립트형 변수

    public GameObject player;
    
    void Awake()
    {
        abilityObject = GameObject.Find("CharacterAbilityObject");
    }
    void Start()
    {
        inv = GameObject.Find("InventoryObject").GetComponent<InventoryScript>();
        tooltip = inv.GetComponent<TooltipScript>();
        player = GameObject.Find("Player");//포션먹을때 확인하기위해
    }
    
    
    public void OnBeginDrag(PointerEventData eventData)//드래그가 시작될때
    {
    
        if (item != null)//아이템이 존재할때
            {
                this.transform.SetParent(this.transform.parent.parent);//슬롯패널의 위치를 가져옴 
                this.transform.position = eventData.position;
                GetComponent<CanvasGroup>().blocksRaycasts = false; //광선 무시를 false시킴으로써 마우스클릭한 부분을 선택할수 있도록함
            }
        
    }

    public void OnDrag(PointerEventData eventData)//드래그중일때
    {
        if (item != null)//아이템이 존재할때
        {
            this.transform.position = eventData.position;// - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)//드래그가 종료되었을때
    {
            this.transform.SetParent(inv.slots[slot].transform);//움직이면 슬롯패널자식으로 사라지는것을 복귀시켜주기위함
            this.transform.position = inv.slots[slot].transform.position;//자리를 벗어나도 원래자리로 돌아오게 하기위함
            GetComponent<CanvasGroup>().blocksRaycasts = true; // 드래그가끝낫을시엔 광선을 무시함으로써 그자리에 있도록함
    }


    public void OnPointerDown(PointerEventData eventData)
    {
   
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null)
            {
                offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);//아이템이 존재할때 오프셋 저장 (현재 들어온 이벤트데이터 포지션에서 
            }
        }
        else if (eventData.button == PointerEventData.InputButton.Right) // 마우스 우측클릭시
        {
            
            if (item != null) // 아이템이 존재하면
            {
               
                if (slot == 0)//슬롯이 0일떄 = 캐릭터장비창의 무기창
                {
                    for (int i = 0; i < 50; i++)
                    {
                        if (inv.slots[i].transform.childCount == 0)
                        {
                            slot = i;
                            //비어있는슬롯을 찾아야함
                            this.transform.SetParent(inv.slots[i].transform);//부모바꿔주기
                            this.transform.position = inv.slots[i].transform.position; //화면상위치바꿔주기

                            inv.items[0] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성  0번째에 아이템 비워주기
                            inv.items[i] = item; // 아이템 추가
                            slot = i;
                            player.GetComponent<WeaponSwitch>().weaponSwitch(inv.items[0]);
                            player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inv.items[0]);
                            abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inv.items[0]);
                            break;
                        }
                    }
                }
                else if (slot != 0)//그냥 인벤토리 슬롯들일경우
                {
                    if(item.Type=="Weapon") //무기일경우 장비교체
                    {
                        if (inv.slots[0].transform.childCount == 0) //무기슬롯에 아이템이 없으면
                        {
                            this.transform.SetParent(inv.slots[0].transform);//부모바꿔주기
                            this.transform.position = inv.slots[0].transform.position; //화면상위치바꿔주기

                            inv.items[slot] = new itemClass();//드랍한 아이템 패널 슬롯에 새로운 아이템 클래스 생성  0번째에 아이템 비워주기
                            inv.items[0] = item; // 아이템 추가
                            slot = 0;
                            player.GetComponent<WeaponSwitch>().weaponSwitch(inv.items[0]);
                            player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inv.items[0]);
                            abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inv.items[0]);
                        }
                        else if(inv.slots[0].transform.childCount != 0) // 장착한 무기가 없을경우  Item은 무기슬롯에있는 것이고 item은 내가 클릭한 슬롯에있는 아이템
                        {
                           
                           Transform Item = inv.slots[0].transform.GetChild(0);//장착무기 슬롯 에있는자식(슬롯패널)의 트랜스폼을 받아옴 offset
                            Item.GetComponent<ItemData>().slot = slot;
                            Item.transform.SetParent(inv.slots[slot].transform);//(부모)슬롯패널의 transform을 받아옴
                            Item.transform.position = inv.slots[slot].transform.position;//드랍할 슬롯의 포지션을 가져옴
                            
                            
                            this.transform.SetParent(inv.slots[0].transform);//부모바꿔주기
                            this.transform.position = inv.slots[0].transform.position; //화면상위치바꿔주기


                            //양쪽아이템 내용물을 서로 바꿈
                            inv.items[slot] = Item.GetComponent<ItemData>().item;//
                            inv.items[0] = item;//
                            slot = 0;
                            player.GetComponent<WeaponSwitch>().weaponSwitch(inv.items[0]);
                            player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inv.items[0]);
                            abilityObject.GetComponent<abilityScript>().PowerTextSwitch(inv.items[0]);

                        }
                        

                    }
                    else if (item.Type == "Food")//포션일때 체력 쳐먹기
                    {
                        if (player.GetComponent<PlayerControll>().isDead == false)
                        {
                            if (player.GetComponent<PlayerControll>().playerHealth > player.GetComponent<PlayerControll>().currentHealth)//만약 피가 풀피면 안쳐먹게 하기위해서
                            {
                                player.GetComponent<PlayerControll>().EatFood(item.Power);//포션은 파워가 회복량 

                                amount--; //양줄이고
                                player.GetComponent<PlayerControll>().potionNumber = amount;//데이터 저장할떄 필요한 포션갯수..
                                this.transform.GetChild(0).GetComponent<Text>().text = amount.ToString();
                                if (amount == 0)//포션 다떨어지면 없애버리자
                                {
                                    Destroy(this.gameObject);
                                    inv.items[slot] = new itemClass();
                                    tooltip.Deactivate();
                                }

                            }
                            else //피가 풀이면안됨
                            {

                                player.GetComponent<PlayerControll>().alarmText("체력이 가득차있습니다.");
                            }
                        }
                        else //뒤지면안됨
                        {
                            player.GetComponent<PlayerControll>().alarmText("죽은상태로는 포션을 먹을 수 없습니다.");
                        }

                    }
                    else // 아이템타입이 아닐경우에는 그냥 쳐먹거나하겠지 나중에 추가해두도록
                    {

                    }
                }


            }

        }


    }




    public void OnPointerEnter(PointerEventData eventData)
    {

        tooltip.Activate(item);//tooltip스크립트에 선언해둔 함수 실행
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }

   
}
/*
Implement Interface'와 'Implement Interface Explicitly'가 있는 걸 볼 수 있습니다. 전자가 우리가 흔히 인터페이스를 구현할 때 써온 암시적 인터페이스 구현이구요. 후자가 여기서 말씀드릴 명시적 인터페이스 구현입니다.
http://www.simpleisbest.net/archive/2008/06/23/2423.aspx 

사실 클래스 라이브러리를 작성하다 보면 인터페이스를 암시적으로 구현하는 것보다 명시적으로 구현하는 것이 좋을 때가 종종 있다. 인터페이스의 구현 사실을 감춤으로써 내가 작성한 클래스 라이브러리를 보다 간단한 모습으로 개발자에게 제공할 수 있을 때가 많기 때문이다.
*/
