using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        if (eventData.pointerDrag != null)
        {
            RectTransform item = eventData.pointerDrag.GetComponent<RectTransform>();

            // Переместить предмет в слот
            item.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // Установить фиксированный размер
            item.sizeDelta = new Vector2(120f, 120f);
        }
    }
}
