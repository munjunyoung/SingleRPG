using UnityEngine;
using System.Collections;

public class GameUIControllScript : MonoBehaviour {
    

    GameObject ExitPanel;
    GameObject InvenPanel;
    GameObject SkillPanel;
    GameObject ShopPanel;
    GameObject StatPanel;
    GameObject SellPanel;
    GameObject BuyPanel;
    GameObject inven;
   

    bool Skillshow;
    bool Statshow;

   
    void Awake()
    {

        InvenPanel = GameObject.Find("Inventory Panel");
     
        SkillPanel = GameObject.Find("SkillWindow Panel");
        BuyPanel = GameObject.Find("BuyPanel");
        SellPanel = GameObject.Find("SellPanel");
        ExitPanel = GameObject.Find("ExitPanel");
        StatPanel = GameObject.Find("StatPanel");
        inven = GameObject.Find("InventoryObject");
        ShopPanel = GameObject.Find("ShopPanel");
        

      
        
    }

    // Use this for initialization
    void Start()
    {
       
        ExitPanel.SetActive(false);
        StatPanel.SetActive(false);
        ShopPanel.SetActive(false);
        BuyPanel.SetActive(false);
        SellPanel.SetActive(false);
        gameObject.SendMessage("ShopOpen");
        gameObject.SendMessage("ShopClose");
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!ExitPanel.activeSelf && !SkillPanel.activeSelf && !InvenPanel.activeSelf &&!ShopPanel.activeSelf&&!StatPanel.activeSelf)
            {
                ExitPanel.SetActive(true);
            }
            else if (InvenPanel.activeSelf)
            {
                InvenPanel.SetActive(false);
            }
            else if(ShopPanel.activeSelf)
            {
                ShopPanel.SetActive(false);
            }
            else if(ExitPanel.activeSelf)
            {
                ExitPanel.SetActive(false);
            }


        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            Skillshow = !Skillshow;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Skillshow)
            {
                Skillshow = !Skillshow;
            }
        }

        if (Skillshow)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }



        if (Input.GetKeyDown(KeyCode.C))
        {
            Statshow = !Statshow;
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Statshow)
            {
                Statshow = !Statshow;
            }
        }

        if (Statshow)
        {
            StatActiate();
        }
        else
        {
            StatDeactivate();
        }


    }

    public void Activate()
    {

        SkillPanel.SetActive(true);
    }

    public void Deactivate()
    {

        SkillPanel.SetActive(false);
    }

    public void StatActiate()
    {
        StatPanel.SetActive(true);
    }
    public void StatDeactivate()
    {
        StatPanel.SetActive(false);
        
    }

    public void ShopClose()
    {

        ShopPanel.SetActive(false);

        if (BuyPanel.activeSelf)
        {
            BuyPanel.SetActive(false);
        }
        if (SellPanel.activeSelf)
        {
            SellPanel.SetActive(false);
        }

        inven.GetComponent<InventoryOnScript>().showinventory = false;
    }

    public void ShopOpen()
    {
        ShopPanel.SetActive(true);

        inven.GetComponent<InventoryOnScript>().showinventory = true;


    }


}
