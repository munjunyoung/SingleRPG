using UnityEngine;
using System.Collections;

public class DurationEffect : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject,1);
        //30초 후 실행됨
    }
}
