using System;
using UnityEngine;
using UnityEngine.UI;

public class Bowl : MonoBehaviour
{
    [SerializeField] public bool isWaterBowl = false;
    [SerializeField] public Sprite filledSprite;
    private Image bowlImage;
    public bool isFilled = false;

    public BowlManager bowlManager; // ��������� ������

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
        Color color = bowlImage.color;
        color.a = 1f;
        bowlImage.color = color;
        isFilled = true;

        Debug.Log($"����� ({(isWaterBowl ? "����" : "���")}) ���������!");

        // ���������� ��������
        if (bowlManager != null)
            bowlManager.CheckBowls();
    }

    public void ResetBowl()
    {
        isFilled = false;
        bowlImage.sprite = null;
        Color color = bowlImage.color;
        color.a = 0f;
        bowlImage.color = color;
    }
}

