using UnityEngine;
using TMPro;
using System.Collections;

public class SafeInput : MonoBehaviour
{
    public TMP_Text displayText;
    public GameObject displayPanel;

    public Canvas closedSafeCanvas;    // Canvas � �������� ������
    public Canvas openedSafeCanvas;    // Canvas � �������� ������

    public string correctCode = "8345";

    private string currentInput = "";
    private bool showingResult = false;

    public float hideDelay = 2f;

    private void Start()
    {
        displayPanel.SetActive(false);
        openedSafeCanvas.gameObject.SetActive(false); // ������ � �������� � ������
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
            displayText.text = "�������!";
            StartCoroutine(SwitchToOpenedCanvasAfterDelay());
        }
        else
        {
            displayText.text = "�������� ���";
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

        // ��������� canvas � �������� ������
        closedSafeCanvas.gameObject.SetActive(false);

        // �������� canvas � �������� ������
        openedSafeCanvas.gameObject.SetActive(true);
    }
}
