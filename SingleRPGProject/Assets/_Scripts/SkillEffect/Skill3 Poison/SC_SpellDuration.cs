using UnityEngine;
using System.Collections;

/**
 * Destroy game object after duration.
 * 
 * @author j@gamemechanix.io
 * @project SpellCraft
 * @copyright GameMechanix.io 2016
 **/
public class SC_SpellDuration : MonoBehaviour {

	public float spellDuration;
    SkillInterFace skill;
    PlayerControll Player;

	void Start () {
        skill = GameObject.Find("SkillObject").GetComponent<SkillInterFace>();
        Player = GameObject.Find("Player").GetComponent<PlayerControll>();//초기화시켜주기위함..

        StartCoroutine(SkillListDestroy(skill.Skills[2].Durationtime));
        Destroy(gameObject, skill.Skills[2].Durationtime);
         //30초 후 실행됨

	}

    IEnumerator SkillListDestroy(float second) //슬라이더바 시간지나면 active off
    {
        yield return new WaitForSeconds(second);
        Player.skillsEffect[2] = null; //비워줌
    }




}
