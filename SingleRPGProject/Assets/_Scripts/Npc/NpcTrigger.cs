using UnityEngine;
using System.Collections;

public class NpcTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other) //들어올때
    {
        if (other.tag == "Player")
        {
          
            this.transform.LookAt(other.transform);
            other.SendMessage("ShopOpen", SendMessageOptions.DontRequireReceiver);

            //   ShopPanel.transform.SetAsLastSibling();


        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            this.transform.LookAt(other.transform);

        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.SendMessage("ShopClose", SendMessageOptions.DontRequireReceiver);
        }
    }


}
