using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IntroPhraseManager : MonoBehaviour
{
    [Header("UI ��������")]
    public Canvas introCanvas;
    public Image background;
    public TMP_Text[] phraseTexts;   // ������ ����� � ��������� ������, �� �������
    public TMP_Text meowText;
    public Button thoughtButton;
    public AudioSource meowAudio;

    [Header("���������")]
    public float typingSpeed = 0.05f;
    public float initialDelay = 0.7f;
    public float fadeOutDuration = 0.5f;
    public float meowDelayBeforeText = 1.5f;  // ������� ������� ��������� ����� ����� "���" ����� �������

    private int currentPhraseIndex = 0;
    private bool phraseFinished = false;

    [Header("����� ��� �����������")]
    public CanvasGroup clickHintGroup;   // <-- ������� CanvasGroup �� "����� ��� �����������"
    public float pulseSpeed = 1.4f;        // �������� ���������
    public float pulseMin = 0.2f;        // ����������� ������������
    public float pulseMax = 0.65f;          // ������������ ������������


    private void Start()
    {
        StartCoroutine(DelayedStart());
    }

    private IEnumerator DelayedStart()
    {
        yield return new WaitForSeconds(initialDelay);

        yield return null;
        phraseTexts[currentPhraseIndex].gameObject.SetActive(true);
        yield return StartCoroutine(TypePhrase(phraseTexts[currentPhraseIndex]));
    }

    private void Update()
    {
        if (phraseFinished && Input.GetMouseButtonDown(0))
        {
            phraseFinished = false;
            StartCoroutine(FadeOutPhrase(phraseTexts[currentPhraseIndex]));

            currentPhraseIndex++;

            if (currentPhraseIndex < phraseTexts.Length)
            {
                phraseTexts[currentPhraseIndex].gameObject.SetActive(true);
                StartCoroutine(TypePhrase(phraseTexts[currentPhraseIndex]));
            }
            else
            {
                StartCoroutine(ShowFinalPart());
            }
        }
    }

    private IEnumerator TypePhrase(TMP_Text text)
    {
        string fullText = text.text;
        text.text = "";

        SetAlpha(text, 1f);

        foreach (char c in fullText)
        {
            text.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        phraseFinished = true;

        // �������� ���������
        yield return new WaitForSeconds(initialDelay);
        clickHintGroup.gameObject.SetActive(true);
        StartCoroutine(PulseHint());
    }

    private IEnumerator FadeOutPhrase(TMP_Text text)
    {
        float elapsed = 0f;
        Color color = text.color;
        float startAlpha = color.a;

        while (elapsed < fadeOutDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(startAlpha, 0f, elapsed / fadeOutDuration);
            text.color = color;
            yield return null;
        }

        color.a = 0f;
        text.color = color;
        text.gameObject.SetActive(false);
    }

    private IEnumerator ShowFinalPart()
    {
        // ����������� ���
        meowAudio.Play();

        // ��� ������� ����� ������� ������
        yield return new WaitForSeconds(meowDelayBeforeText);

        meowText.gameObject.SetActive(true);

        yield return new WaitForSeconds(1.5f);
        thoughtButton.gameObject.SetActive(true);
    }

    private void SetAlpha(TMP_Text text, float alpha)
    {
        Color c = text.color;
        c.a = alpha;
        text.color = c;
    }

    private IEnumerator PulseHint()
    {
        float t = 0f;

        while (phraseFinished) // ���������� ���� ������������ �� �����
        {
            t += Time.deltaTime * pulseSpeed;
            float alpha = Mathf.Lerp(pulseMin, pulseMax, (Mathf.Sin(t) + 1f) / 2f); // ������� �������������
            clickHintGroup.alpha = alpha;

            yield return null;
        }

        // ��� ������ ������������ ����� � ��������
        clickHintGroup.gameObject.SetActive(false);
    }

}
