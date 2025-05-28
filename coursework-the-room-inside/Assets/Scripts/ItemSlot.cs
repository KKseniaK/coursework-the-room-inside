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
            DragDrop dragItem = eventData.pointerDrag.GetComponent<DragDrop>();
            RectTransform item = eventData.pointerDrag.GetComponent<RectTransform>();

            // Если в слоте уже есть предмет, меняем их местами
            if (transform.childCount > 0)
            {
                Transform existingItem = transform.GetChild(0);
                existingItem.SetParent(dragItem.GetCurrentSlot().transform, false);
                existingItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                existingItem.GetComponent<DragDrop>().SetSlot(dragItem.GetCurrentSlot());
                dragItem.GetCurrentSlot().ResetSlotColor();
            }

            // Устанавливаем новый предмет в слот
            item.SetParent(transform, false);
            item.anchoredPosition = Vector2.zero;
            item.sizeDelta = new Vector2(120f, 120f);

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
