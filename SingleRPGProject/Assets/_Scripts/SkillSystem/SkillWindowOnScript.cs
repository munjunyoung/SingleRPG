using UnityEngine;
using System.Collections;

public class SkillWindowOnScript : MonoBehaviour
{
    GameObject SkillWindow;
    bool show;


    // Use this for initialization
    void Start()
    {
        SkillWindow = GameObject.Find("SkillWindow Panel");
    }

    // Update is called once per frame
    void Update()
    {

        /*    if(Input.GetKeyDown(KeyCode.K))
            {
                show = !show;
            }

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(show)
                {
                    show = !show;
                }
            }

            if(show)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }


        }

        public void Activate()
        {

            SkillWindow.SetActive(true);
        }

        public void Deactivate()
        {

            SkillWindow.SetActive(false);
        }*/
    }
}
