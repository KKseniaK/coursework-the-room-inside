using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class DragDropTests
{
    private GameObject canvasGO;
    private Canvas canvas;
    private GameObject eventSystemGO;

    [SetUp]
    public void SetUp()
    {
        // Create Canvas
        canvasGO = new GameObject("Canvas", typeof(Canvas), typeof(CanvasScaler), typeof(GraphicRaycaster));
        canvas = canvasGO.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        // Create EventSystem
        eventSystemGO = new GameObject("EventSystem", typeof(EventSystem), typeof(StandaloneInputModule));
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(canvasGO);
        Object.DestroyImmediate(eventSystemGO);
    }


    [UnityTest]
    public IEnumerator OnDrag_MovesObject()
    {
        var slotGO = new GameObject("Slot", typeof(RectTransform), typeof(ItemSlot));
        slotGO.transform.SetParent(canvasGO.transform);

        var dragGO = CreateDraggable(slotGO.transform);
        var dragDrop = dragGO.GetComponent<DragDrop>();

        var beginDragData = new PointerEventData(EventSystem.current);
        dragDrop.OnBeginDrag(beginDragData);

        var dragData = new PointerEventData(EventSystem.current)
        {
            delta = new Vector2(100, 50)
        };

        dragDrop.OnDrag(dragData);
        yield return null;

        Assert.AreNotEqual(Vector2.zero, dragGO.GetComponent<RectTransform>().anchoredPosition);
    }

    [UnityTest]
    public IEnumerator OnEndDrag_WaterStream_FillsGlass()
    {
        var water = new GameObject("WaterStream");
        water.tag = "WaterStream";

        var dragGO = CreateDraggable(canvasGO.transform, addGlass: true);
        var dragDrop = dragGO.GetComponent<DragDrop>();
        var glass = dragGO.GetComponent<Glass>();

        var data = new PointerEventData(EventSystem.current)
        {
            pointerCurrentRaycast = new RaycastResult { gameObject = water }
        };

        dragDrop.OnEndDrag(data);
        yield return null;

        Assert.IsFalse(glass.isFilled);
    }

    [UnityTest]
    public IEnumerator OnEndDrag_Bowl_AttemptFillCalled()
    {
        var bowlGO = new GameObject("Bowl", typeof(Bowl));
        var bowl = bowlGO.GetComponent<Bowl>();
        bowl.wasFillAttempted = false;

        var dragGO = CreateDraggable(canvasGO.transform);
        var dragDrop = dragGO.GetComponent<DragDrop>();

        var data = new PointerEventData(EventSystem.current)
        {
            pointerCurrentRaycast = new RaycastResult { gameObject = bowlGO }
        };

        dragDrop.OnEndDrag(data);
        yield return null;

        Assert.IsFalse(bowl.wasFillAttempted);
    }

    [UnityTest]
    public IEnumerator OnEndDrag_EmptyHit_ReturnsToSlot()
    {
        var slotGO = new GameObject("Slot", typeof(RectTransform), typeof(ItemSlot));
        slotGO.transform.SetParent(canvasGO.transform);

        var dragGO = CreateDraggable(slotGO.transform);
        var dragDrop = dragGO.GetComponent<DragDrop>();

        var data = new PointerEventData(EventSystem.current)
        {
            pointerCurrentRaycast = new RaycastResult { gameObject = null }
        };

        dragDrop.OnEndDrag(data);
        yield return null;

        Assert.AreEqual(slotGO.transform, dragGO.transform.parent);
    }

    // --- HELPER METHODS ---

    private GameObject CreateDraggable(Transform parent, bool addGlass = false)
    {
        var dragGO = new GameObject("DragItem", typeof(RectTransform), typeof(CanvasGroup), typeof(Image), typeof(DragDrop));
        dragGO.transform.SetParent(parent, false);

        var dragDrop = dragGO.GetComponent<DragDrop>();
        //dragDrop.SetSlot(parent.GetComponent<ItemSlot>());
        dragDrop.GetComponent<CanvasGroup>().blocksRaycasts = true;
        dragDrop.GetComponent<Image>().color = Color.white;
        dragDrop.GetType().GetField("canvas", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.SetValue(dragDrop, canvas);

        if (addGlass)
        {
            dragGO.AddComponent<Glass>();
        }

        return dragGO;
    }

    // --- MOCK CLASSES ---

    public class Bowl : MonoBehaviour
    {
        public bool wasFillAttempted = false;

        public void TryFill(GameObject item)
        {
            wasFillAttempted = true;
        }
    }

    public class Glass : MonoBehaviour
    {
        public bool isFilled = false;

        public void FillWithWater()
        {
            isFilled = true;
        }
    }

    public class ItemSlot : MonoBehaviour
    {
        private Image img;

        private void Awake()
        {
            img = gameObject.AddComponent<Image>();
        }

        public void SetSlotColor(Color color)
        {
            img.color = color;
        }

        public void ResetSlotColor()
        {
            img.color = Color.white;
        }
    }
}
