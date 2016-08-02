using UnityEngine;
using System.Collections;

public class Sliderbar : MonoBehaviour {

   //위치할 타겟설정
    GameObject enemyPosition;
    public int id; // 슬라이더 아이디
    EnemyInsControll EnemyIns;
   

    void Start()
    {
       
        EnemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();
        enemyPosition = EnemyIns.EnemyList[id];
    }

	
	// Update is called once per frame
	void Update () {

        if (enemyPosition) //게임오브젝트가 존재할때만 실행 
        {
            Vector3 pos = Camera.main.WorldToScreenPoint(enemyPosition.transform.position); //적의 월드좌표를  화면좌표로 변환

            transform.position = new Vector3(pos.x, pos.y + 100.0f, 0);
        }
            //guiCam.ScreenToWorldPoint(new Vector3(pos.x, pos.y + 60.0f, 0));//화면좌표에서 월드좌표로 변환
        
        

    }

    public void EnemySliderDestroy(int id)
    {
      
      
    }
}
