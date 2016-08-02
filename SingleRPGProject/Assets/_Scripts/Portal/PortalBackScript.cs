using UnityEngine;
using System.Collections;

public class PortalBackScript : MonoBehaviour {
    GameObject player;
    GameControllScript gamecontroll;
    EnemyInsControll insclear;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        gamecontroll = GameObject.Find("GameControllerObject").GetComponent<GameControllScript>();

    }

    void OnTriggerEnter(Collider other)
    {
        if (Application.loadedLevelName == "Level 01")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 01");
                player.transform.position = new Vector3(0, 0, 0);

            }
        }
        else if (Application.loadedLevelName == "Level 02")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 01");
                player.transform.localPosition = new Vector3(-25, 1, 83.5f);

            }

        }
        else if (Application.loadedLevelName == "Level 03")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 02");
                player.transform.localPosition = new Vector3(28.7f, 1f, -24f);

            }

        }
        else if (Application.loadedLevelName == "Level 04")
        {
            if (other.tag == "Player")
            {
                Application.LoadLevel("Level 03");
                player.transform.localPosition = new Vector3(0f, 1f, 115f);

            }

        }
    }
}
