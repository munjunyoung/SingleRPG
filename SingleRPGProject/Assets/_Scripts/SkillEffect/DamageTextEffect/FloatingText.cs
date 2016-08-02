using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FloatingText : MonoBehaviour {
    public Animator animator;
    Text damgeText;
   
	// Use this for initialization
	void OnEnable() {
        AnimatorClipInfo[] clipinfo = animator.GetCurrentAnimatorClipInfo(0);
        Destroy(this.gameObject, clipinfo[0].clip.length);
        damgeText = animator.GetComponent<Text>();
	}
    
	
	public void SetText(string Text)
    {
        damgeText.text = Text;
    }
}
