using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;              // ������ �������������
    public float fadeDuration = 0.5f;    // ������������ ��������
    [Range(0f, 1f)]
    public float maxFadeAlpha = 0.8f;    // ������������ ������� ����������
    public Canvas[] wallCanvases;        // ��� �����
    private int currentWallIndex = 0;
    private bool isFading = false;

    void Start()
    {
        SetAlpha(0f);
        fadeImage.raycastTarget = false;
    }

    public void SwitchWall(int direction)
    {
        if (!isFading)
            StartCoroutine(FadeAndSwitch(direction));
    }

    private IEnumerator FadeAndSwitch(int direction)
    {
        isFading = true;
        fadeImage.raycastTarget = true;

        // ���������
        yield return StartCoroutine(Fade(0f, maxFadeAlpha, fadeDuration));

        // ����� �������
        wallCanvases[currentWallIndex].gameObject.SetActive(false);
        wallCanvases[direction].gameObject.SetActive(true);
        currentWallIndex = direction;

        // ����������
        yield return StartCoroutine(Fade(maxFadeAlpha, 0f, fadeDuration));

        fadeImage.raycastTarget = false;
        isFading = false;
    }

    private IEnumerator Fade(float from, float to, float duration)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        c.a = from;
        fadeImage.color = c;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Lerp(from, to, elapsed / duration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = to;
        fadeImage.color = c;
    }

    private void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}
