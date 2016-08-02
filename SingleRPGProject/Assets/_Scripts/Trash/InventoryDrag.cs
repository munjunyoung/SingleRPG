using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using System;

public class InventoryDrag : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    GameObject inven;
    Vector2 offset;
    
    bool select;

    // Use this for initialization
    void Start()
    {
        inven = GameObject.Find("Inventory Panel");


    }
    

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            select = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            select = false;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)//드래그가 시작될때
    {
        Debug.Log("1");
        if (select)
        {
            Debug.Log("1");
            this.transform.position = eventData.position;
            GetComponent<CanvasGroup>().blocksRaycasts = false; //광선 무시를 false시킴으로써 마우스클릭한 부분을 선택할수 있도록함
        }
    }

    public void OnDrag(PointerEventData eventData)//드래그중일때
    {

        if (select)
        {
            Debug.Log("2");
            this.transform.position = eventData.position - offset;
        }
        
    }

    public void OnEndDrag(PointerEventData eventData)//드래그가 종료되었을때
    {
            select = false;
            GetComponent<CanvasGroup>().blocksRaycasts = true; // 드래그가끝낫을시엔 광선을 무시함으로써 그자리에 있도록함

        this.transform.position = this.transform.position * 2;

    }
}
