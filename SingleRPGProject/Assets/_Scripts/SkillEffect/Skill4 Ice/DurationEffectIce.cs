using UnityEngine;
using System.Collections;

public class DurationEffectIce : MonoBehaviour {

    public float spellDuration;
    SkillInterFace skill;
    PlayerControll Player;
 


    void Start()
    {
        skill = GameObject.Find("SkillObject").GetComponent<SkillInterFace>();
        Player = GameObject.Find("Player").GetComponent<PlayerControll>();//초기화시켜주기위함..
    

        StartCoroutine(SkillListDestroy(skill.Skills[3].Durationtime - 1));
        Destroy(gameObject, skill.Skills[3].Durationtime);

        
        //30초 후 실행됨

    }

    IEnumerator SkillListDestroy(float second) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);
        this.gameObject.GetComponent<CapsuleCollider>().radius = 0.1f;
        Player.skillsEffect[3] = null; //비워줌
   
    }


}
