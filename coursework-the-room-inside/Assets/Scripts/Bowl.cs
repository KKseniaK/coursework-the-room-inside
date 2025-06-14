using UnityEngine;
using UnityEngine.UI;

public class Bowl : MonoBehaviour
{
    [SerializeField] private bool isWaterBowl = false;
    [SerializeField] private Sprite filledSprite;
    private Image bowlImage;
    private bool isFilled = false;

    private void Awake()
    {
        bowlImage = GetComponent<Image>();
    }

    public void TryFill(GameObject draggedItem)
    {
        if (isFilled) return;

        if (isWaterBowl && draggedItem.CompareTag("FilledGlass"))
        {
            FillBowl();
            Destroy(draggedItem);
        }
        else if (!isWaterBowl && draggedItem.CompareTag("FoodItem"))
        {
            FillBowl();
            Destroy(draggedItem);
        }
    }

    private void FillBowl()
    {
        bowlImage.sprite = filledSprite;
        isFilled = true;
        Debug.Log($"Миска ({(isWaterBowl ? "вода" : "еда")}) наполнена!");
    }
}
