using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class IntroPhraseManager : MonoBehaviour
{
    [Header("UI элементы")]
    public Canvas introCanvas;
    public Image background;
    public TMP_Text[] phraseTexts;   // Каждая фраза — отдельный объект, по порядку
    public TMP_Text meowText;
    public Button thoughtButton;
    public AudioSource meowAudio;

    [Header("Настройки")]
    public float typingSpeed = 0.05f;
    public float initialDelay = 0.7f;
    public float fadeOutDuration = 0.5f;
    public float meowDelayBeforeText = 1.5f;  // Сколько времени подождать после звука "мяу" перед текстом

    private int currentPhraseIndex = 0;
    private bool phraseFinished = false;

    [Header("Нажми для продолжения")]
    public CanvasGroup clickHintGroup;   // <-- назначь CanvasGroup от "Нажми для продолжения"
    public float pulseSpeed = 1.4f;        // скорость пульсации
    public float pulseMin = 0.2f;        // минимальная прозрачность
    public float pulseMax = 0.65f;          // максимальная прозрачность


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

        // Включаем подсказку
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
        // Проигрываем мяу
        meowAudio.Play();

        // Ждём немного перед выводом текста
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

        while (phraseFinished) // пульсирует пока пользователь не нажал
        {
            t += Time.deltaTime * pulseSpeed;
            float alpha = Mathf.Lerp(pulseMin, pulseMax, (Mathf.Sin(t) + 1f) / 2f); // плавное пульсирование
            clickHintGroup.alpha = alpha;

            yield return null;
        }

        // как только пользователь нажал — скрываем
        clickHintGroup.gameObject.SetActive(false);
    }

}
