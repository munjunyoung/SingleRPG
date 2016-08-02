using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveControll : MonoBehaviour
{

    [Serializable]
    public class PlayerData
    {//플레이어 필요한정보 레벨, 아이템, 경험치, 돈, 스킬배운것들, 스킬창유지
        public float level;
        public float exp;
        public float gold;
        public int itemNumber;
        public int potionNumber;
        public List<int> itemID = new List<int>();
        public List<bool> SkillOpen = new List<bool>();

        public float statpoint;
        public float powerStat;
        public float healthstat;
        public float speedStat;


       
    }

    public static PlayerData pData;
    public static bool setting;

    public static void SaveData()
    {
        PlayerPrefs.DeleteAll();
        BinaryFormatter bf = new BinaryFormatter();
        GameObject player = GameObject.Find("Player");
        GameObject inven = GameObject.Find("InventoryObject");
        // FileStream file = File.Create(Application.persistentDataPath + "/PlayerData.dat");
        MemoryStream ms = new MemoryStream();
        pData = new PlayerData();
        pData.level = player.GetComponent<PlayerControll>().PlayerLevel;
        pData.exp = player.GetComponent<PlayerControll>().PlayerCurrentExPoint;
        pData.gold = player.GetComponent<PlayerControll>().playerGold;
        pData.itemNumber = inven.GetComponent<InventoryScript>().playerTotalItemNumber;
        pData.potionNumber = player.GetComponent<PlayerControll>().potionNumber;

       
        
        for (int i = 0; i <50; i++)
        {
            
            pData.itemID.Add(new int());
            if (inven.GetComponent<InventoryScript>().items[i].ID != -1)
            {
                
                    pData.itemID[i] = inven.GetComponent<InventoryScript>().items[i].ID;
                    pData.itemID[i] = inven.GetComponent<InventoryScript>().items[i].ID;
                  
            }
            else
            {
                pData.itemID[i] = -1;
            }
        
        }
        for (int j=0; j<7;j++) //스킬 오픈된것 저장
        {
            pData.SkillOpen.Add(new bool());
            pData.SkillOpen[j] = player.GetComponent<PlayerControll>().wSkillOn[j];
        }

        pData.statpoint = player.GetComponent<PlayerControll>().statPoint;
        pData.powerStat = player.GetComponent<PlayerControll>().powerStat;
        pData.healthstat = player.GetComponent<PlayerControll>().healthStat;
        pData.speedStat = player.GetComponent<PlayerControll>().SpeedStat;



        bf.Serialize(ms, pData); //시리얼화


        PlayerPrefs.SetString("Data1", Convert.ToBase64String(ms.GetBuffer()));
    }

    public static void LoadData()
    {
        var msdata = PlayerPrefs.GetString("Data1");

        if (!string.IsNullOrEmpty(msdata))
        {


            SaveControll.setting = true;//로드설정 온
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream(Convert.FromBase64String(msdata));
            //  FileStream file = File.Open(Application.persistentDataPath + "/PlayerData.dat", FileMode.Open);
            // Debug.Log(bf.Deserialize(ms));

            pData = (PlayerData)bf.Deserialize(ms);

        }
    }

    public static void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }

    public static void SceneData()
    {
        
    }
}