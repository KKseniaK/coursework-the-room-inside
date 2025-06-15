using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Image itemImage;
    [SerializeField] private Color selectedColor = Color.gray;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private ItemSlot currentSlot;

    // ������ canvas ��������� ��� ������
    public Canvas Canvas
    {
        get => canvas;
        set => canvas = value;
    }
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

        // ������� ������� ����
        currentSlot = GetComponentInParent<ItemSlot>();

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;

        // ��������� ��������, ����� ������� �� ��� ��������� ��������� �����
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        GameObject hitObject = eventData.pointerCurrentRaycast.gameObject;

        // === ��������� �� ����� ���� ===
        if (hitObject != null && hitObject.CompareTag("WaterStream"))
        {
            Glass glass = GetComponent<Glass>();
            if (glass != null && !glass.isFilled)
            {
                glass.FillWithWater();
            }

            ReturnToSlot();
            return;
        }

        // === ��������� �� ����� ===
        if (hitObject != null && hitObject.GetComponent<Bowl>() != null)
        {
            Bowl bowl = hitObject.GetComponent<Bowl>();
            bowl.TryFill(gameObject);
            return;
        }

        // === ������ �� ������ ===
        ReturnToSlot();
    }

    public void ReturnToSlot()
    {
        if (currentSlot != null)
        {
            transform.SetParent(currentSlot.transform, false);
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    public void SetSlot(ItemSlot slot)
    {
        currentSlot = slot;
    }

    // ����������� ����� ��� ��������� �������� �����
    public ItemSlot GetCurrentSlot()
    {
        return currentSlot;
    }
}
