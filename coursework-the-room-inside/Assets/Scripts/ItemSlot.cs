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
            // Если в слоте уже есть предмет — выходим
            if (transform.childCount > 0)
            {
                Debug.Log("Слот уже занят. Действие отменено.");
                return;
            }

            DragDrop dragItem = eventData.pointerDrag.GetComponent<DragDrop>();
            RectTransform item = eventData.pointerDrag.GetComponent<RectTransform>();
            RectTransform slotRect = GetComponent<RectTransform>();

            // Устанавливаем новый предмет в слот
            item.SetParent(transform, false);
            item.anchoredPosition = Vector2.zero;

            // Масштабируем с сохранением пропорций до 80% размера слота
            Vector2 slotSize = slotRect.rect.size;
            Vector2 originalSize = item.sizeDelta;

            if (originalSize.x == 0 || originalSize.y == 0)
            {
                Debug.LogWarning("Размер объекта равен нулю. Масштабирование невозможно.");
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
