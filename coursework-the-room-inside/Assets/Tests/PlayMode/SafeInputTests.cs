using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class SafeInputTests
{
    private GameObject testObj;
    private SafeInput safeInput;

    private Text displayText;
    private GameObject displayPanel;
    private Canvas closedCanvas;
    private Canvas openedCanvas;

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        testObj = new GameObject("SafeInputObj");

        safeInput = testObj.AddComponent<SafeInput>();

        // Создание нужных компонентов
        var displayPanelObj = new GameObject("DisplayPanel");
        displayPanel = displayPanelObj;
        displayPanelObj.SetActive(true); // чтобы мы проверили поведение

        var displayTextObj = new GameObject("DisplayText", typeof(Text));
        displayText = displayTextObj.GetComponent<Text>();

        var closedCanvasObj = new GameObject("ClosedCanvas", typeof(Canvas));
        closedCanvas = closedCanvasObj.GetComponent<Canvas>();

        var openedCanvasObj = new GameObject("OpenedCanvas", typeof(Canvas));
        openedCanvas = openedCanvasObj.GetComponent<Canvas>();

        // Присваиваем поля
        safeInput.displayPanel = displayPanel;
        safeInput.displayText = displayText;
        safeInput.closedSafeCanvas = closedCanvas;
        safeInput.openedSafeCanvas = openedCanvas;
        safeInput.correctCode = "1234";
        safeInput.hideDelay = 0.1f;

        safeInput.Invoke("Start", 0); // вызов protected Start()

        yield return null;
    }

    [UnityTest]
    public IEnumerator AppendDigit_AddsDigitAndUpdatesText()
    {
        safeInput.AppendDigit("1");
        Assert.AreEqual("1", displayText.text);
        Assert.IsTrue(displayPanel.activeSelf);

        safeInput.AppendDigit("2");
        Assert.AreEqual("12", displayText.text);
        yield return null;
    }

    [UnityTest]
    public IEnumerator DeleteLastDigit_RemovesDigit()
    {
        safeInput.AppendDigit("1");
        safeInput.AppendDigit("2");
        safeInput.DeleteLastDigit();
        Assert.AreEqual("1", displayText.text);
        yield return null;
    }

    [UnityTest]
    public IEnumerator CheckCode_Correct_ShowsSuccessAndSwitchesCanvas()
    {
        safeInput.correctCode = "42";
        safeInput.AppendDigit("4");
        safeInput.AppendDigit("2");

        safeInput.CheckCode();

        Assert.AreEqual("Открыто!", displayText.text);
        yield return new WaitForSeconds(0.2f);

        Assert.IsFalse(closedCanvas.gameObject.activeSelf);
        Assert.IsTrue(openedCanvas.gameObject.activeSelf);
    }

    [UnityTest]
    public IEnumerator CheckCode_Wrong_ShowsErrorAndClears()
    {
        safeInput.AppendDigit("9");
        safeInput.AppendDigit("9");

        safeInput.CheckCode();

        Assert.AreEqual("Неверный код", displayText.text);
        yield return new WaitForSeconds(0.2f);
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        Object.Destroy(testObj);
        Object.Destroy(displayPanel);
        Object.Destroy(displayText.gameObject);
        Object.Destroy(closedCanvas.gameObject);
        Object.Destroy(openedCanvas.gameObject);
        yield return null;
    }
}
