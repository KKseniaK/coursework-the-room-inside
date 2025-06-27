using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PawAnimationController : MonoBehaviour
{
    public Image fadeBackground;   // ������ ���
    public Image[] pawSteps;       // �����
    public float initialDelay = 0.5f;
    public float fadeToBlackDuration = 1f;
    public float pawFadeDuration = 0.5f;
    public float delayBetweenPaws = 0.8f;

    public Canvas BG;
    public Canvas PawCanvas;
    public event Action AnimationCompleted;
    public void StartSequence()
    {     
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        // �������� ����� ������� ����������
        if (initialDelay > 0f)
            yield return new WaitForSeconds(initialDelay);

        // ���������� ������
        yield return StartCoroutine(FadeImage(fadeBackground, 0f, 1f, fadeToBlackDuration));

        // ��������� ������ UI
        if (BG != null)
            BG.gameObject.SetActive(false);

        // ��������� �����
        foreach (var paw in pawSteps)
        {
            yield return StartCoroutine(FadeImage(paw, 0f, 1f, pawFadeDuration));
            yield return new WaitForSeconds(delayBetweenPaws);
        }

        // ������������ �����
        foreach (var paw in pawSteps)
        {
            yield return StartCoroutine(FadeImage(paw, 1f, 0f, pawFadeDuration));
        }

        AnimationCompleted?.Invoke();

        // �������� ����� UI
        if (BG != null)
            BG.gameObject.SetActive(true);

        // ���������� (������� ����������)
        yield return StartCoroutine(FadeImage(fadeBackground, 1f, 0f, fadeToBlackDuration));

        // �������� ���� canvas
        PawCanvas.gameObject.SetActive(false);
    }

    private IEnumerator FadeImage(Image img, float from, float to, float duration)
    {
        float elapsed = 0f;
        Color color = img.color;
        color.a = from;
        img.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(from, to, elapsed / duration);
            img.color = color;
            yield return null;
        }

        color.a = to;
        img.color = color;
    }
}

