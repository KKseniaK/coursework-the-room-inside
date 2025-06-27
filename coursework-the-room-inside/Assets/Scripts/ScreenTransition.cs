using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenTransition : MonoBehaviour
{
    public RectTransform fadeImage;
    public float slideDuration = 0.6f;
    public float holdDuration = 0.2f;

    private Vector2 offscreenLeft;
    private Vector2 center;
    private Vector2 offscreenRight;

    public void Start()
    {
        float width = fadeImage.rect.width;
        center = Vector2.zero;
        offscreenLeft = new Vector2(-width, 0);
        offscreenRight = new Vector2(width, 0);

        fadeImage.anchoredPosition = offscreenLeft; // Стартовая позиция
    }

    public void PlayTransition(bool fromRight, System.Action onMidpoint = null)
    {
        StartCoroutine(TransitionCoroutine(fromRight, onMidpoint));
    }

    private IEnumerator TransitionCoroutine(bool fromRight, System.Action onMidpoint)
    {
        Vector2 start = fromRight ? offscreenRight : offscreenLeft;
        Vector2 end = center;

        fadeImage.anchoredPosition = start;

        float elapsed = 0f;
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.anchoredPosition = Vector2.Lerp(start, end, elapsed / slideDuration);
            yield return null;
        }

        fadeImage.anchoredPosition = end;

        yield return new WaitForSeconds(holdDuration);

        // Возврат — уход экрана
        elapsed = 0f;
        while (elapsed < slideDuration)
        {
            elapsed += Time.deltaTime;
            fadeImage.anchoredPosition = Vector2.Lerp(end, start, elapsed / slideDuration);
            yield return null;
        }

        fadeImage.anchoredPosition = start;
    }
}

