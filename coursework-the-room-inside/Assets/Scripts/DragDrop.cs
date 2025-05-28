using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image itemImage; // Ссылка на Image компонент предмета
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color selectedColor = Color.gray;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private ItemSlot currentSlot;
    private bool isDragging = false;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (itemImage == null)
            itemImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("OnPointerDown");

        // Находим текущий слот
        currentSlot = GetComponentInParent<ItemSlot>();

        if (currentSlot != null)
        {
            // Делаем слот серым
            currentSlot.SetSlotColor(selectedColor);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        isDragging = true;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // Отключаем родителя, чтобы предмет не был ограничен границами слота
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        isDragging = false;
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        // Если предмет не был помещен в другой слот, возвращаем его в исходный
        if (eventData.pointerCurrentRaycast.gameObject == null ||
            eventData.pointerCurrentRaycast.gameObject.GetComponent<ItemSlot>() == null)
        {
            ReturnToSlot();
        }
    }

    public void ReturnToSlot()
    {
        if (currentSlot != null)
        {
            transform.SetParent(currentSlot.transform, false);
            rectTransform.anchoredPosition = Vector2.zero;
            currentSlot.ResetSlotColor();
        }
    }

    public void SetSlot(ItemSlot slot)
    {
        currentSlot = slot;
    }

    // Добавленный метод для получения текущего слота
    public ItemSlot GetCurrentSlot()
    {
        return currentSlot;
    }
}
