using UnityEngine;
using TMPro;
using System.Collections;

public class SafeInput : MonoBehaviour
{
    public TMP_Text displayText;
    public GameObject displayPanel;

    public Canvas closedSafeCanvas;    // Canvas с закрытым сейфом
    public Canvas openedSafeCanvas;    // Canvas с открытым сейфом

    public string correctCode = "8345";

    private string currentInput = "";
    private bool showingResult = false;

    public float hideDelay = 2f;

    private void Start()
    {
        displayPanel.SetActive(false);
        openedSafeCanvas.gameObject.SetActive(false); // открыт — выключен в начале
    }

    public void AppendDigit(string digit)
    {
        if (showingResult) return;

        if (currentInput.Length < 8)
        {
            currentInput += digit;
            UpdateDisplay();
            if (!displayPanel.activeSelf)
                displayPanel.SetActive(true);
        }
    }

    public void DeleteLastDigit()
    {
        if (showingResult) return;

        if (currentInput.Length > 0)
        {
            currentInput = currentInput.Substring(0, currentInput.Length - 1);
            UpdateDisplay();
        }
    }

    public void CheckCode()
    {
        if (showingResult) return;

        if (!displayPanel.activeSelf)
            displayPanel.SetActive(true);

        if (currentInput == correctCode)
        {
            displayText.text = "Открыто!";
            StartCoroutine(SwitchToOpenedCanvasAfterDelay());
        }
        else
        {
            displayText.text = "Неверный код";
            StartCoroutine(ClearAndHideAfterError());
        }

        showingResult = true;
    }

    private void UpdateDisplay()
    {
        displayText.text = currentInput;
    }

    private IEnumerator ClearAndHideAfterError()
    {
        yield return new WaitForSeconds(hideDelay);
        currentInput = "";
        showingResult = false;
        displayPanel.SetActive(false);
    }

    private IEnumerator SwitchToOpenedCanvasAfterDelay()
    {
        yield return new WaitForSeconds(hideDelay);

        // Выключаем canvas с закрытым сейфом
        closedSafeCanvas.gameObject.SetActive(false);

        // Включаем canvas с открытым сейфом
        openedSafeCanvas.gameObject.SetActive(true);
    }
}
