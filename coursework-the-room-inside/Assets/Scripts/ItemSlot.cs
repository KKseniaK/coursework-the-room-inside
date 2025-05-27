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

            // ����������� ������� � ����
            item.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

            // ���������� ������������� ������
            item.sizeDelta = new Vector2(120f, 120f);
        }
    }
}
