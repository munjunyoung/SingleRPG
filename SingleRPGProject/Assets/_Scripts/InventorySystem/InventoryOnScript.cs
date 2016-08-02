using UnityEngine;
using System.Collections;

public class InventoryOnScript : MonoBehaviour
{ 

    GameObject inventoryPanel;
    public GameObject tooltip;
    public bool showinventory;
    Vector3 currentPosition;
    Vector3 HidePosition;

    bool select;
    Vector2 offset;
    // Use this for initialization
    void Awake () {
        
        inventoryPanel = GameObject.Find("Inventory Panel");
        tooltip = GameObject.Find("TooltipImageItem");
       
        
    
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.I)) 
        {
            showinventory = !showinventory;
        }
        
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(showinventory)
            {
                showinventory = false;
            }
        }


        if (showinventory)
        {
            Activate();
           
        }
        else if (!showinventory)
        {

            tooltip.SetActive(false);

            Deactivate();
        }
    }

    public void Activate()
    {

        inventoryPanel.SetActive(true);
      
        
       
    
        
    }
    //setactive false를 할경우 인벤토리를 키지 않을때 함수가실행이안됨..
    // enalbe과 alpha는 인벤토리 오프일때도 아이템이 클릭이동이 가능함.. 결국 포지션을 변경해서 서로 바꿔주는것으로 설정..
    public void Deactivate()
    {
      
        inventoryPanel.SetActive(false);
    
    }



  


}