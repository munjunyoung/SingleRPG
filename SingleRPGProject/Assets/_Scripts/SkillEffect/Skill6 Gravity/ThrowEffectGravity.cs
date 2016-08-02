using UnityEngine;
using System.Collections;

public class ThrowEffectGravity : MonoBehaviour {
    GameObject SkillPrefab;
    GameObject Skill;
    EnemyInsControll enemyIns;



    // Use this for initialization
    void Awake()
    {
        SkillPrefab = Resources.Load<GameObject>("SkillEffect/Skill6EffectAfter") as GameObject;
        enemyIns = GameObject.Find("EnemyControllObject").GetComponent<EnemyInsControll>();

    }
    void Start()
    {
        Skill = null;
    }
    // Update is called once per frame
    void Update () {
        transform.Translate(0, 0, 3*Time.deltaTime);
        Destroy(this.gameObject, 10f);
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (Skill == null)
            {
                Skill = Instantiate(SkillPrefab);
                Skill.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                Skill.transform.localScale = Vector3.one;
                Skill.transform.localRotation = Quaternion.Euler(0, 0, 0);
                other.GetComponent<EnemyController>().TakeDamage(35);
            }

            Destroy(this.gameObject);
        }
        else if(other.tag == "Boss")
        {
            if (Skill == null)
            {
                Skill = Instantiate(SkillPrefab);
                Skill.transform.localPosition = new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z);
                Skill.transform.localScale = Vector3.one;
                Skill.transform.localRotation = Quaternion.Euler(0, 0, 0);
                other.GetComponent<BossController>().TakeDamage(35);
            }

            Destroy(this.gameObject);
        }
    }
}
