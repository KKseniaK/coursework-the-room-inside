using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CatWalkSequence : MonoBehaviour
{
    [SerializeField] private Image background;
    [SerializeField] private Image[] pawPrints;
    [SerializeField] private float delayBetweenSteps = 0.3f;
    [SerializeField] private float fadeDuration = 0.5f;

    public void PlaySequence()
    {
        StartCoroutine(PlayCatWalk());
    }

    private IEnumerator PlayCatWalk()
    {
        // Дать Unity один кадр на отрисовку Canvas
        yield return null;

        Debug.Log("Начинаем fade черного фона");

        yield return StartCoroutine(FadeImage(background, 0f, 1f, fadeDuration));

        foreach (var paw in pawPrints)
        {
            paw.color = new Color(1f, 1f, 1f, 1f);
            yield return new WaitForSeconds(delayBetweenSteps);
        }

        yield return new WaitForSeconds(0.8f);

        foreach (var paw in pawPrints)
        {
            paw.color = new Color(1f, 1f, 1f, 0f);
        }

        yield return StartCoroutine(FadeImage(background, 1f, 0f, fadeDuration));

        gameObject.SetActive(false);
    }


    private IEnumerator FadeImage(Image image, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = image.color;

        while (elapsed < duration)
        {
            float alpha = Mathf.Lerp(from, to, elapsed / duration);
            image.color = new Color(color.r, color.g, color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        image.color = new Color(color.r, color.g, color.b, to);
    }
}
