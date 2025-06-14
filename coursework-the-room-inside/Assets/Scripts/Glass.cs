using UnityEngine;
using UnityEngine.UI;

public class Glass : MonoBehaviour
{
    public bool isFilled = false;
    [SerializeField] private Sprite filledSprite;

    public void FillWithWater()
    {
        if (isFilled) return;

        isFilled = true;
        GetComponent<Image>().sprite = filledSprite;
        gameObject.tag = "FilledGlass";
        Debug.Log("Стакан наполнен водой");
    }
}
