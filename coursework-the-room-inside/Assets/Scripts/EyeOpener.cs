using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EyeOpener : MonoBehaviour
{
    public RectTransform upperLid;
    public RectTransform lowerLid;
    public float openDuration = 1.2f;

    private Vector2 upperStart;
    private Vector2 lowerStart;

    private void Start()
    {
        // Сохраняем начальные позиции
        upperStart = upperLid.anchoredPosition;
        lowerStart = lowerLid.anchoredPosition;
    }

    public void OpenEyes()
    {
        StartCoroutine(OpenEyesRoutine());
    }

    private IEnumerator OpenEyesRoutine()
    {
        float elapsed = 0f;
        float maxOffset = upperLid.rect.height; // Насколько поднять/опустить

        while (elapsed < openDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / openDuration;

            upperLid.anchoredPosition = Vector2.Lerp(upperStart, upperStart + new Vector2(0f, maxOffset), t);
            lowerLid.anchoredPosition = Vector2.Lerp(lowerStart, lowerStart - new Vector2(0f, maxOffset), t);

            yield return null;
        }

        // Полностью убираем
        upperLid.gameObject.SetActive(false);
        lowerLid.gameObject.SetActive(false);
    }
}

