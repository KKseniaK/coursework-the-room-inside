using UnityEngine;
using UnityEngine.EventSystems;

public class ClockHandDraggable : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    public Transform pivot; // Центр вращения — ClockPivot
    public bool isHourHand = false; // Для логики взаимодействия

    private void Start()
    {
        if (pivot == null)
            pivot = transform.parent;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Можно сделать звук или визуал начала вращения
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pivotScreenPos = RectTransformUtility.WorldToScreenPoint(eventData.pressEventCamera, pivot.position);
        Vector2 direction = eventData.position - pivotScreenPos;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

