using UnityEngine;
using System.Collections;


public class GameControllScript : MonoBehaviour {

    EnemyInsControll enemyins;
    Transform[] SpawnPoint = new Transform[10];

    GameObject player;
    GameObject camera;
    GameObject UICamera;
    GameObject canvas;
    GameObject EventSystem;
    GameObject InventoryObject;
    GameObject EnemyControllerObject;
    GameObject skillObject;
    GameObject wayPoint;

    bool start;


    void Awake()
    {
        EnemyControllerObject = GameObject.Find("EnemyControllObject");
        InventoryObject = GameObject.Find("InventoryObject");
        enemyins = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();

    }

	// Use this for initialization
	public void Start () {


        
       
      //  InvokeRepeating("Spawn", 3f, 3f);
        
          
       
    }

    void OnLevelWasLoaded(int level)
    {
        if (level == 1)
        {
            SpawnPoint[0] = GameObject.Find("wayPoint 0").GetComponent<Transform>();
            SpawnPoint[1] = GameObject.Find("wayPoint 1").GetComponent<Transform>();
            SpawnPoint[2] = GameObject.Find("wayPoint 2").GetComponent<Transform>();
            SpawnPoint[3] = GameObject.Find("wayPoint 3").GetComponent<Transform>();
            SpawnPoint[4] = GameObject.Find("wayPoint 4").GetComponent<Transform>();
            SpawnPoint[5] = GameObject.Find("wayPoint 5").GetComponent<Transform>();
            SpawnPoint[6] = GameObject.Find("wayPoint 6").GetComponent<Transform>();
            SpawnPoint[7] = GameObject.Find("wayPoint 7").GetComponent<Transform>();
            SpawnPoint[8] = GameObject.Find("wayPoint 8").GetComponent<Transform>();
            SpawnPoint[9] = GameObject.Find("wayPoint 9").GetComponent<Transform>();
            EnemyControllerObject.GetComponent<EnemyInsControll>().enemyClear();
            InventoryObject.GetComponent<DropItemOnScript>().DropObjClear();
      
            enemyins.SpawnEnemy(SpawnPoint[0].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[2].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[4].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[6].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[8].position, 1);
        }
        else if (level == 2)
        {
            SpawnPoint[0] = GameObject.Find("wayPoint 0").GetComponent<Transform>();
            SpawnPoint[1] = GameObject.Find("wayPoint 1").GetComponent<Transform>();
            SpawnPoint[2] = GameObject.Find("wayPoint 2").GetComponent<Transform>();
            SpawnPoint[3] = GameObject.Find("wayPoint 3").GetComponent<Transform>();
            SpawnPoint[4] = GameObject.Find("wayPoint 4").GetComponent<Transform>();
            SpawnPoint[5] = GameObject.Find("wayPoint 5").GetComponent<Transform>();
            SpawnPoint[6] = GameObject.Find("wayPoint 6").GetComponent<Transform>();
            SpawnPoint[7] = GameObject.Find("wayPoint 7").GetComponent<Transform>();
            SpawnPoint[8] = GameObject.Find("wayPoint 8").GetComponent<Transform>();
            SpawnPoint[9] = GameObject.Find("wayPoint 9").GetComponent<Transform>();
            EnemyControllerObject.GetComponent<EnemyInsControll>().enemyClear();
            InventoryObject.GetComponent<DropItemOnScript>().DropObjClear();

            enemyins.SpawnEnemy(SpawnPoint[0].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[2].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[4].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[6].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[8].position, 2);
        }
        else if (level == 3)
        {
            
            SpawnPoint[0] = GameObject.Find("wayPoint 0").GetComponent<Transform>();
            SpawnPoint[1] = GameObject.Find("wayPoint 1").GetComponent<Transform>();
            SpawnPoint[2] = GameObject.Find("wayPoint 2").GetComponent<Transform>();
            SpawnPoint[3] = GameObject.Find("wayPoint 3").GetComponent<Transform>();
            SpawnPoint[4] = GameObject.Find("wayPoint 4").GetComponent<Transform>();
            SpawnPoint[5] = GameObject.Find("wayPoint 5").GetComponent<Transform>();
            SpawnPoint[6] = GameObject.Find("wayPoint 6").GetComponent<Transform>();
            SpawnPoint[7] = GameObject.Find("wayPoint 7").GetComponent<Transform>();
            SpawnPoint[8] = GameObject.Find("wayPoint 8").GetComponent<Transform>();
            SpawnPoint[9] = GameObject.Find("wayPoint 9").GetComponent<Transform>();
            EnemyControllerObject.GetComponent<EnemyInsControll>().enemyClear();
            InventoryObject.GetComponent<DropItemOnScript>().DropObjClear();
            
            enemyins.SpawnEnemy(SpawnPoint[0].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[1].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[2].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[3].position, 3);
            enemyins.SpawnEnemy(SpawnPoint[4].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[5].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[6].position, 1);
            enemyins.SpawnEnemy(SpawnPoint[7].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[8].position, 2);
            enemyins.SpawnEnemy(SpawnPoint[9].position, 3);
        }
        else if(level ==4)
        {
            EnemyControllerObject.GetComponent<EnemyInsControll>().enemyClear();
            InventoryObject.GetComponent<DropItemOnScript>().DropObjClear();
        }
        else if (level == 5)
        {
            EnemyControllerObject.GetComponent<EnemyInsControll>().enemyClear();
            InventoryObject.GetComponent<DropItemOnScript>().DropObjClear();
        }

    }

}
