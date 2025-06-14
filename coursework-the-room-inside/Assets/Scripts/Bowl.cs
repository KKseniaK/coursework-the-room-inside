using UnityEngine;
using UnityEngine.UI;

public class Bowl : MonoBehaviour
{
    [SerializeField] private bool isWaterBowl = false;
    [SerializeField] private Sprite filledSprite;
    private Image bowlImage;
    public bool isFilled = false;

    private void Awake()
    {
        bowlImage = GetComponent<Image>();
    }

    public void TryFill(GameObject draggedItem)
{
    if (isFilled || draggedItem == null) return;

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
        bowlImage.color = new Color(1f, 1f, 1f, 1f); // Сделать видимым
        isFilled = true;
        Debug.Log($"Миска ({(isWaterBowl ? "вода" : "еда")}) наполнена!");

        // Проверка состояния всех мисок
        FindObjectOfType<BowlManager>().CheckBowls();
    }
}
