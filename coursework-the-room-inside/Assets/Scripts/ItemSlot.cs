using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    [SerializeField] private Image slotImage;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color emptyColor = Color.gray;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            // ���� � ����� ��� ���� ������� � �������
            if (transform.childCount > 0)
            {
                Debug.Log("���� ��� �����. �������� ��������.");
                return;
            }

            DragDrop dragItem = eventData.pointerDrag.GetComponent<DragDrop>();
            RectTransform item = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform slotRect = GetComponent<RectTransform>();

            // ������������� ����� ������� � ����
            item.SetParent(transform, false);
            item.anchoredPosition = Vector2.zero;

            // ������������ � ����������� ��������� �� 80% ������� �����
            Vector2 slotSize = slotRect.rect.size;
            Vector2 originalSize = item.sizeDelta;

            if (originalSize.x == 0 || originalSize.y == 0)
            {
                Debug.LogWarning("������ ������� ����� ����. ��������������� ����������.");
            }
            else
            {
                float scaleFactor = 0.8f * Mathf.Min(slotSize.x / originalSize.x, slotSize.y / originalSize.y);
                Vector2 newSize = originalSize * scaleFactor;
                item.sizeDelta = newSize;
            }

            dragItem.SetSlot(this);
            ResetSlotColor();
        }
    }



    public void SetSlotColor(Color color)
    {
        if (slotImage != null)
            slotImage.color = color;
    }

    public void ResetSlotColor()
    {
        if (slotImage != null)
            slotImage.color = normalColor;
    }

    public void SetEmptyColor()
    {
        if (slotImage != null)
            slotImage.color = emptyColor;
    }

    
}
