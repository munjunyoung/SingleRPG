using UnityEngine;
using System.Collections;

public class PortalScript : MonoBehaviour {
    GameObject player;
    GameObject Camera;
    
	// Use this for initialization
	void Start () {
   
       
	}
   

    void OnTriggerEnter(Collider other)
    {
        if (Application.loadedLevelName == "Level 01")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 02");
                player = GameObject.Find("Player");
                Camera = GameObject.Find("Main Camera");
                player.transform.position = new Vector3(5, 0, 0);
                Camera.GetComponent<CameraFollow>().currentAngleX = 90;
                Camera.GetComponent<CameraFollow>().currentAngleY = 30;
            }
        }
        else if(Application.loadedLevelName =="Level 02")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 03");
                player = GameObject.Find("Player");
                Camera = GameObject.Find("Main Camera");
                player.transform.position = new Vector3(0, 0, 5);
                player.transform.localRotation = Quaternion.Euler(Vector3.zero);
                Camera.GetComponent<CameraFollow>().currentAngleX = 360;
                Camera.GetComponent<CameraFollow>().currentAngleY = 30;

            }

        }
        else if (Application.loadedLevelName == "Level 03")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 04");
                player = GameObject.Find("Player");
                player.transform.position = new Vector3(0, 0, 5);
               
            }

        }
    }
}
