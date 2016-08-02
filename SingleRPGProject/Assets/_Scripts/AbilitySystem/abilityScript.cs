using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class abilityScript : MonoBehaviour {
    PlayerControll player;
    GameObject StatPanel;
    GameObject inven;

    void Awake()
    {
        player = GameObject.Find("Player").GetComponent<PlayerControll>();
        StatPanel = GameObject.Find("StatPanel");
        inven = GameObject.Find("InventoryObject");

    }
	// Use this for initialization
	void Start () {
      


    }
	

    public void PowerUPButton()
    {
        if (player.statPoint > 0)
        {
            player.powerStat++;
            player.statPoint--;
            player.attackPower = player.powerStat * 5;
            player.GetComponentInChildren<PlayerAttack>().DamageSwitch(inven.GetComponent<InventoryScript>().items[0]);
            StatPanel.transform.FindChild("StatGridPanel").transform.FindChild("PowerPanel").transform.FindChild("PowerStatImage").transform.FindChild("PowerStatText").GetComponent<Text>().text = "" + player.powerStat;
            StatPanel.transform.FindChild("StatGridPanel").transform.FindChild("PowerPanel").transform.FindChild("PowerExText").GetComponent<Text>().text = "공격력 +" + player.attackPower + "" + "무기공 " + inven.GetComponent<InventoryScript>().items[0].Power;
            StatPanel.transform.FindChild("StatabilityPanel").transform.FindChild("StatImage").transform.FindChild("CurrentStatText").GetComponent<Text>().text = "" + player.statPoint;
        }
        else
        {
            player.GetComponent<PlayerControll>().alarmText("스탯포인트가 부족합니다");
        }
    }
    public void HealthUP()
    {
        if (player.statPoint > 0)
        {
            player.healthStat++;
            player.statPoint--;
            player.Health = player.healthStat * 10;
            player.healthSetting();
            StatPanel.transform.FindChild("StatGridPanel").transform.Find("HpPanel").transform.Find("HpStatImage").transform.Find("HpStatText").GetComponent<Text>().text = "" + player.healthStat;
            StatPanel.transform.FindChild("StatGridPanel").transform.Find("HpPanel").transform.Find("HpExText").GetComponent<Text>().text = "체력 +" + player.Health + "전체 +" + ((player.PlayerLevel+1) * 50);

            StatPanel.transform.FindChild("StatabilityPanel").transform.FindChild("StatImage").transform.FindChild("CurrentStatText").GetComponent<Text>().text = "" + player.statPoint;
        }
        else
        {
            player.GetComponent<PlayerControll>().alarmText("스탯포인트가 부족합니다");
        }
    }

    public void SpeedUP()
    {
        if (player.statPoint > 0)
        {
            player.SpeedStat++;
            player.statPoint--;
            player.AttackSpeed = 1.5f + (player.SpeedStat * 0.1f);
            player.anim.SetFloat("attackSpeed", player.AttackSpeed);
            StatPanel.transform.FindChild("StatGridPanel").transform.Find("ASpeedPanel").transform.Find("ASpeedStatImage").transform.Find("ASpeedStatText").GetComponent<Text>().text = "" + player.SpeedStat;
            StatPanel.transform.FindChild("StatGridPanel").transform.Find("ASpeedPanel").transform.Find("ASpeedExText").GetComponent<Text>().text = "일반공속 : " + "1.5" + " + " + player.SpeedStat * 0.1f;

            StatPanel.transform.FindChild("StatabilityPanel").transform.FindChild("StatImage").transform.FindChild("CurrentStatText").GetComponent<Text>().text = "" + player.statPoint;
        }
        else
        {
            player.GetComponent<PlayerControll>().alarmText("스탯포인트가 부족합니다");
        }
    }

    public void PowerTextSwitch(itemClass item)
    {
        StatPanel.transform.FindChild("StatGridPanel").transform.FindChild("PowerPanel").transform.FindChild("PowerExText").GetComponent<Text>().text = "일반공 +" + player.attackPower + "" + "무기공 +" +item.Power;

    }


    public void SettingCharacterPanel()
    {
        StatPanel.transform.FindChild("StatGridPanel").transform.FindChild("PowerPanel").transform.FindChild("PowerStatImage").transform.FindChild("PowerStatText").GetComponent<Text>().text = "" + player.powerStat;
        StatPanel.transform.FindChild("StatGridPanel").transform.FindChild("PowerPanel").transform.FindChild("PowerExText").GetComponent<Text>().text = "일반공 +" + player.attackPower + "" + "무기공 +" + inven.GetComponent<InventoryScript>().items[0].Power;

        StatPanel.transform.FindChild("StatGridPanel").transform.Find("HpPanel").transform.Find("HpStatImage").transform.Find("HpStatText").GetComponent<Text>().text = "" + player.healthStat;
        StatPanel.transform.FindChild("StatGridPanel").transform.Find("HpPanel").transform.Find("HpExText").GetComponent<Text>().text = "체력 +" + player.Health+"전체 +" + (((player.PlayerLevel + 1) * 50));

        StatPanel.transform.FindChild("StatGridPanel").transform.Find("ASpeedPanel").transform.Find("ASpeedStatImage").transform.Find("ASpeedStatText").GetComponent<Text>().text = "" + player.SpeedStat;
        StatPanel.transform.FindChild("StatGridPanel").transform.Find("ASpeedPanel").transform.Find("ASpeedExText").GetComponent<Text>().text = "일반공속 : "+"1.5"+" + " + player.SpeedStat*0.1f;


        StatPanel.transform.FindChild("StatabilityPanel").transform.FindChild("StatImage").transform.FindChild("CurrentStatText").GetComponent<Text>().text = "" + player.statPoint;
    }

    public void UpdateAbilityPoint()
    {

        StatPanel.transform.FindChild("StatabilityPanel").transform.FindChild("StatImage").transform.FindChild("CurrentStatText").GetComponent<Text>().text = "" + player.statPoint;
    }
}
