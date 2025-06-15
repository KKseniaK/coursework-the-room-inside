using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;
using System.Collections;

public class ItemSlotTests
{
    private GameObject slotObj;
    private ItemSlot slot;
    private Image slotImage;
    private GameObject dragItemObj;
    private DragDrop dragItem;
    private RectTransform itemRect;

    [SetUp]
    public void Setup()
    {
        // Создаем слот
        slotObj = new GameObject("Slot");
        slot = slotObj.AddComponent<ItemSlot>();
        slotObj.AddComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        slotImage = slotObj.AddComponent<Image>();

        // Инициализируем цвета
        slot.normalColor = Color.white;
        slot.emptyColor = Color.gray;

        // Создаем тестовый перетаскиваемый объект
        dragItemObj = new GameObject("DragItem");
        dragItem = dragItemObj.AddComponent<DragDrop>();
        itemRect = dragItemObj.AddComponent<RectTransform>();
        itemRect.sizeDelta = new Vector2(50, 50);
        dragItemObj.AddComponent<CanvasRenderer>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(slotObj);
        Object.DestroyImmediate(dragItemObj);
    }

    [UnityTest]
    public IEnumerator OnDrop_AcceptsItem_WhenSlotEmpty()
    {
        // Arrange
        var eventData = new PointerEventData(EventSystem.current)
        {
            pointerDrag = dragItemObj
        };

        // Act
        slot.OnDrop(eventData);
        yield return null;

        // Assert
        Assert.AreEqual(slotObj.transform, dragItemObj.transform.parent);
        Assert.AreEqual(Vector2.zero, itemRect.anchoredPosition);
        Assert.AreEqual(Color.white, slotImage.color);
    }

    [UnityTest]
    public IEnumerator OnDrop_RejectsItem_WhenSlotOccupied()
    {
        // Arrange - помещаем другой объект в слот
        var occupiedObj = new GameObject("OccupiedItem");
        occupiedObj.transform.SetParent(slotObj.transform);

        var eventData = new PointerEventData(EventSystem.current)
        {
            pointerDrag = dragItemObj
        };

        // Act
        slot.OnDrop(eventData);
        yield return null;

        // Assert
        LogAssert.Expect(LogType.Log, "Слот уже занят. Действие отменено.");
        Assert.AreNotEqual(slotObj.transform, dragItemObj.transform.parent);
    }

    [Test]
    public void OnDrop_ScalesItem_To80PercentOfSlotSize()
    {
        // Arrange
        slotObj.GetComponent<RectTransform>().sizeDelta = new Vector2(100, 100);
        itemRect.sizeDelta = new Vector2(50, 50);

        var eventData = new PointerEventData(EventSystem.current)
        {
            pointerDrag = dragItemObj
        };

        // Act
        slot.OnDrop(eventData);

        // Assert
        Assert.AreEqual(new Vector2(80, 80), itemRect.sizeDelta);
    }

    [Test]
    public void OnDrop_LogsWarning_WhenItemSizeZero()
    {
        // Arrange
        itemRect.sizeDelta = Vector2.zero;

        var eventData = new PointerEventData(EventSystem.current)
        {
            pointerDrag = dragItemObj
        };

        // Act
        slot.OnDrop(eventData);

        // Assert
        LogAssert.Expect(LogType.Warning, "Размер объекта равен нулю. Масштабирование невозможно.");
    }
}