using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveData : MonoBehaviour {

    GameObject player;
    GameObject inven;

   

    void Awake()
    {
        player = GameObject.Find("Player");
        inven = GameObject.Find("InventoryObject");
        Data.Add(new PlayerData());
    }


    [Serializable]
    public class PlayerData
    {//플레이어 필요한정보 레벨, 아이템, 경험치, 돈, 스킬배운것들, 스킬창유지
        public float level;
        public float exp;
        public float gold;
        public int itemNumber;
        public List<int> itemID = new List<int>();
    }

    public static List<PlayerData> Data = new List<PlayerData>();

    public void SaveState()
    {
        PlayerPrefs.DeleteAll();
        BinaryFormatter bf = new BinaryFormatter();

        // FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");
        MemoryStream ms = new MemoryStream();
        Data.Add(new PlayerData());
        Data[0].level = player.GetComponent<PlayerControll>().PlayerLevel;
        Data[0].exp = player.GetComponent<PlayerControll>().PlayerCurrentExPoint;
        Data[0].gold = player.GetComponent<PlayerControll>().playerGold;
        Data[0].itemNumber = inven.GetComponent<InventoryScript>().playerTotalItemNumber;

        Debug.Log(Data[0].level);
        Debug.Log(Data[0].exp);
        Debug.Log(Data[0].gold);
        Debug.Log("item 갯수 :"+Data[0].itemNumber);
        for (int i = 0; i < Data[0].itemNumber; i++)
        {

            Data[0].itemID.Add(new int());
            Data[0].itemID[i] = inven.GetComponent<InventoryScript>().items[i].ID;
            Debug.Log(i+" 번째 item id : "+ Data[0].itemID[i]);
        }
        

        bf.Serialize(ms, Data); //시리얼화
       

        PlayerPrefs.SetString("Data1", Convert.ToBase64String(ms.GetBuffer()));
    }


    public void LoadState()
    {
       //PlayerPrefs.DeleteAll();
        var msdata = PlayerPrefs.GetString("Data1");
      
        if (!string.IsNullOrEmpty(msdata))
        {

            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(msdata));
            //  FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
            // Debug.Log(bf.Deserialize(ms));

            Data = (List<PlayerData>)bf.Deserialize(ms);

            Debug.Log(Data[0].level);
            Debug.Log(Data[0].exp);
            Debug.Log(Data[0].gold);
            Debug.Log(Data[0].itemNumber);

            player.GetComponent<PlayerControll>().PlayerLevel = Data[0].level;
            player.GetComponent<PlayerControll>().PlayerCurrentExPoint = Data[0].exp;
            player.GetComponent<PlayerControll>().playerGold = (int)Data[0].gold;
            player.GetComponent<PlayerControll>().itemNumber = Data[0].itemNumber;
            for (int i=0;i<Data[0].itemNumber; i++)
            {
                player.GetComponent<PlayerControll>().itemid.Add(Data[0].itemID[i]);
               
            }
           

        }
        else
        {
            Debug.Log("data가음성");
            
            player.GetComponent<PlayerControll>().itemNumber = 1;
            player.GetComponent<PlayerControll>().itemid.Add(3);
            player.GetComponent<PlayerControll>().PlayerLevel = 1;
            player.GetComponent<PlayerControll>().PlayerCurrentExPoint = 0;
            player.GetComponent<PlayerControll>().playerGold = 0;
        }
    // Application.LoadLevel("Level 01");

    }
    

   



}



///파일스트림
    /*

    public void SaveState()
    {
        player = GameObject.Find("Player");
        inven = GameObject.Find("InventoryObject");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");

        data = new PlayerData();

        data.level = player.GetComponent<PlayerControll>().PlayerLevel;
        data.exp = player.GetComponent<PlayerControll>().PlayerCurrentExPoint;
        data.gold = player.GetComponent<InventoryScript>().playerGold;


        bf.Serialize(file, data); //시리얼화
        file.Close();//파일저장닫기
    }
    public void LoadState()
    {
        if(File.Exists(Application.persistentDataPath + "/PlayerData.dat"))//파일이 존재하면
        {
            Application.LoadLevel("Level 01");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat",FileMode.Open);
            data = (PlayerData)bf.Deserialize(file);
            file.Close();

            player.GetComponent<PlayerControll>().PlayerLevel = data.level;
            player.GetComponent<PlayerControll>().PlayerCurrentExPoint = data.exp;
            inven.GetComponent<InventoryScript>().playerGold = (int)data.gold;

        }
        else
        {
        /*    player.GetComponent<PlayerControll>().PlayerLevel = 1;
            player.GetComponent<PlayerControll>().PlayerCurrentExPoint = 0;
            player.GetComponent<InventoryScript>().playerGold = 0;
        }
       
    }

    public void NewStart()
    {
        data = new PlayerData();
        data.level = 1;
        data.exp = 0;
        data.gold = 0;
        Application.LoadLevel("Level 01");
          player.GetComponent<PlayerControll>().PlayerLevel = 1;
          player.GetComponent<PlayerControll>().PlayerCurrentExPoint = 0;
          player.GetComponent<InventoryScript>().playerGold = 0;
      
        
    }
}




[Serializable]
public class PlayerData
{//플레이어 필요한정보 레벨, 아이템, 경험치, 돈, 스킬배운것들, 스킬창유지
    public float level;
    public float exp;
    public float gold;
}*/

