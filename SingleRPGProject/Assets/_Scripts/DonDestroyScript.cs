using UnityEngine;
using System.Collections;

public class DonDestroyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        
        if(Application.loadedLevelName=="Level 00")
        {
            Destroy(this.gameObject);
        }
    
	
	}
}
