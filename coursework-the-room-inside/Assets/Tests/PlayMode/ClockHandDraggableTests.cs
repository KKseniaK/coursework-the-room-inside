using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;

public class ClockHandDraggableTests
{
    [UnityTest]
    public IEnumerator Pivot_Assigned_From_Parent_If_Null()
    {
        var parent = new GameObject("Pivot");
        var hand = new GameObject("Hand");
        hand.transform.SetParent(parent.transform);

        var draggable = hand.AddComponent<ClockHandDraggable>();

        yield return null; // Дать сработать Start()

        Assert.AreEqual(parent.transform, draggable.pivot, "pivot должен быть установлен в transform.parent");
    }

    [UnityTest]
    public IEnumerator OnDrag_Rotates_ClockHand()
    {
        // 1. Setup pivot and hand
        var pivotGO = new GameObject("Pivot", typeof(RectTransform));
        pivotGO.transform.position = new Vector3(0, 0, 0);

        var handGO = new GameObject("Hand", typeof(RectTransform));
        handGO.transform.SetParent(pivotGO.transform);
        handGO.transform.position = new Vector3(0, 1, 0); // above pivot

        var draggable = handGO.AddComponent<ClockHandDraggable>();
        draggable.pivot = pivotGO.transform;

        yield return null; // wait a frame

        // 2. Simulate drag to the right of the pivot (should rotate to -90°)
        var eventData = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(100, 0)
        };

        // Camera is null in tests
        RectTransformUtility.WorldToScreenPoint(null, draggable.pivot.position);

        draggable.OnDrag(eventData);

        float zRotation = handGO.transform.eulerAngles.z;

        // Because we're dragging to the right of pivot (positive X), expect -90° which becomes 270°
        Assert.That(zRotation, Is.InRange(269f, 271f), $"Ожидался поворот ~270°, но получено {zRotation}");
    }

}
