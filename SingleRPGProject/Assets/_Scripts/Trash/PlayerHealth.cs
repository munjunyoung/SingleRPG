using UnityEngine;
using System.Collections;
//만들어야 할것 플레이어 피
public class PlayerHealth : MonoBehaviour {
    int playerHealth=100;
    int currentHealth;
    bool isDead; //플레이어 사망
    public Transform Enemy;
    Animation anim;
	// Use this for initialization
	void Start () {
        currentHealth = playerHealth;
        anim = GetComponent<Animation>();
	}


    public void TakeDamage(int damageAmount)
    {
        Debug.Log(currentHealth);
        if(isDead)
        {
            return;
        }
        currentHealth -= damageAmount;
        transform.LookAt(Enemy);
        anim.CrossFade("resist", 0.25f);
        if(currentHealth<=0)
        {
            Death();
        }
    }
	

    void Death() //죽음
    {
        isDead = true;
        anim.CrossFade("die", 0.01f);
    }
	// Update is called once per frame
	void Update () {

	}
}
