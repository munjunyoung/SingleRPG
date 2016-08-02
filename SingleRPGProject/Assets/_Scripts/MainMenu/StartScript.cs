using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScript : MonoBehaviour {

    public GameObject All;
    public GameObject alarmPanel;

    private float alarmTimer;

    void Start()
    {
        alarmPanel.SetActive(false);
    }
	// Update is called once per frame
	void Update () {


	    if (alarmPanel.activeSelf)
	    {
	        alarmTimer += Time.deltaTime;
	    }

	    if (alarmTimer > 3)
	    {
	        if (alarmPanel.activeSelf)
	        {
	            alarmPanel.SetActive(false);
	        }
	    }
	}

    public void NewButton()
    {
        SaveControll.setting = false;
        Application.LoadLevel("Level 01");
        Instantiate(All);
    }
    public void LoadButton()
    {
        SaveControll.LoadData();
        if (SaveControll.pData != null)
        {
           
            Application.LoadLevel("Level 01");
            Instantiate(All);
        }
        else
        {
           alarmText("로드할 데이터가 없습니다.");
        }
    }

    public void DeleteButton()
    {
        SaveControll.DeleteData();
        alarmText("저장된 데이터를 삭제합니다.");
    }

    public void ExitButton()
    {
        Application.Quit();
    }


    public void alarmText(string data)
    {
        alarmPanel.SetActive(true);
        alarmPanel.GetComponentInChildren<Text>().text = data;
        alarmTimer = 0;
    }
}
