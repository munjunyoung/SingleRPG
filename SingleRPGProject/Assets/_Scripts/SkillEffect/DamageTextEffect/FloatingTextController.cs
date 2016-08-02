using UnityEngine;
using System.Collections;

public class FloatingTextController : MonoBehaviour {
    private static GameObject popupTextPrefab;
    private static GameObject Canvasparent;

	public static void Initialize()
    {
        Canvasparent = GameObject.Find("PopUpObject");
        if(!popupTextPrefab)
        {
            popupTextPrefab = Resources.Load<GameObject>("SkillEffect/PopUpEffect");
        }
    }

    public static void CreatePopText(string text, Vector3 position)
    {
        GameObject popUpText = Instantiate(popupTextPrefab);
        Vector2 screenPostion = Camera.main.WorldToScreenPoint(position);


        popUpText.transform.localPosition = screenPostion;
        popUpText.transform.localScale = Vector3.one;
        popUpText.GetComponent<FloatingText>().SetText(text);
    }
}

