using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyInsControll : MonoBehaviour {
    public List<GameObject> EnemyList = new List<GameObject>();//적을 받아올 리스트 아이디 부여
    public List<GameObject> EnemySliderList = new List<GameObject>();//오브젝트를 넣어둘 리스트
    public GameObject EnemySliderPrefab; //적슬라이더를 받아올 오브젝트
    public GameObject EnemyPrefab; //적오브젝트를 받아올 오브젝트
    public GameObject EnemyPrefab2;
    public GameObject EnemyPrefab3;
    
    GameObject spawnObjectParent;//적오브젝트가 생성되면 부모가 되는 오브젝트
    GameObject spawnSliderParent;//적슬라이더오브젝트가 생성되면 부모가 되는 오브젝트
    Transform spawnPosition;


    public int enemyNumber = 0;

	// Use this for initialization
	void Start () {
        spawnObjectParent = GameObject.Find("EnemyControllObject");
        spawnSliderParent = GameObject.Find("EnemyHealthObject");
       // spawnPosition = GameObject.Find("wayPoint 0").GetComponent<Transform>();
     
    }
	
	// Update is called once per frame
	void Update () {

	
	}

    public void SpawnEnemy(Vector3 SpawnPosition, int enemykind)//enemy종류는 1= 고블린 근접 2= 해골근접 3= 원거리 고블린
    {
        if (enemykind == 1)
        {



            EnemyList.Add(Instantiate(EnemyPrefab2));
            EnemySliderList.Add(Instantiate(EnemySliderPrefab));


            //  EnemyList[enemyNumber].transform.SetParent(spawnObjectParent.transform);//오브젝트가 생성되면 부모의 자식으로 설정
            EnemyList[enemyNumber].transform.position = SpawnPosition; //스폰하는 포지션 설정
            EnemyList[enemyNumber].transform.localScale = Vector3.one * 1.2f;
            EnemyList[enemyNumber].GetComponent<EnemyController>().id = enemyNumber;//생성된 에너미의 id설정 슬라이더가 따라가기위함
            EnemyList[enemyNumber].GetComponentInChildren<EnemyAttack>().id = enemyNumber;//자식스크립트의 id도 다설정..  마법을쓰는놈일경우 이거 필요없음***********
            EnemyList[enemyNumber].GetComponentInChildren<AggroCheck>().id = enemyNumber;//


            EnemySliderList[enemyNumber].transform.SetParent(spawnSliderParent.transform);//생성되어질 위치 부모설정
            EnemySliderList[enemyNumber].transform.localScale = Vector3.one;
            EnemySliderList[enemyNumber].GetComponent<Sliderbar>().id = enemyNumber;//슬라이더바 id설정 
            enemyNumber++;
        }
        else if (enemykind == 2)
        {
            //에너미 오브젝트 생성


            EnemyList.Add(Instantiate(EnemyPrefab));
            EnemySliderList.Add(Instantiate(EnemySliderPrefab));


            //  EnemyList[enemyNumber].transform.SetParent(spawnObjectParent.transform);//오브젝트가 생성되면 부모의 자식으로 설정
            EnemyList[enemyNumber].transform.position = SpawnPosition; //스폰하는 포지션 설정
            EnemyList[enemyNumber].transform.localScale = Vector3.one * 1.2f;
            EnemyList[enemyNumber].GetComponent<EnemyController>().id = enemyNumber;//생성된 에너미의 id설정 슬라이더가 따라가기위함
            EnemyList[enemyNumber].GetComponentInChildren<EnemyAttack>().id = enemyNumber;//자식스크립트의 id도 다설정.. 
            EnemyList[enemyNumber].GetComponentInChildren<AggroCheck>().id = enemyNumber;//


            EnemySliderList[enemyNumber].transform.SetParent(spawnSliderParent.transform);//생성되어질 위치 부모설정
            EnemySliderList[enemyNumber].transform.localScale = Vector3.one;
            EnemySliderList[enemyNumber].GetComponent<Sliderbar>().id = enemyNumber;//슬라이더바 id설정 

            enemyNumber++;

        }
        else if (enemykind == 3) 
        {
            EnemyList.Add(Instantiate(EnemyPrefab3));
            EnemySliderList.Add(Instantiate(EnemySliderPrefab));


            //  EnemyList[enemyNumber].transform.SetParent(spawnObjectParent.transform);//오브젝트가 생성되면 부모의 자식으로 설정
            EnemyList[enemyNumber].transform.position = SpawnPosition; //스폰하는 포지션 설정
            EnemyList[enemyNumber].transform.localScale = Vector3.one * 1.2f;
            EnemyList[enemyNumber].GetComponent<EnemyController>().id = enemyNumber;//생성된 에너미의 id설정 슬라이더가 따라가기위함
            //EnemyList[enemyNumber].GetComponentInChildren<EnemyAttack>().id = enemyNumber;//자식스크립트의 id도 다설정..  마법을쓰는놈일경우 이거 필요없음***********
            EnemyList[enemyNumber].GetComponentInChildren<AggroCheck>().id = enemyNumber;//


            EnemySliderList[enemyNumber].transform.SetParent(spawnSliderParent.transform);//생성되어질 위치 부모설정
            EnemySliderList[enemyNumber].transform.localScale = Vector3.one;
            EnemySliderList[enemyNumber].GetComponent<Sliderbar>().id = enemyNumber;//슬라이더바 id설정 
            enemyNumber++;
        }


    }

    public void enemyClear()
    {
        for (int i = 0; i < EnemySliderList.Count; i++)//젠될때 슬라이더는 canvas에 존재하므로 삭제되지않음 그러므로 확인하고 삭제함
        {
            if (EnemySliderList[i] != null)
            {
                Destroy(EnemySliderList[i]);
            }
        }

        EnemyList.Clear();
        EnemySliderList.Clear();
        enemyNumber = 0;
    }
}
