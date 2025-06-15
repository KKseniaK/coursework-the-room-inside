using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // --- Кнопка "Start Game" в Main Menu ---
    public void StartGame()
    {
        LoadSceneWithTransition("Bedroom");
    }

    // --- Кнопка "Open Game Menu" в Bedroom ---
    public void OpenGameMenu()
    {
        LoadSceneWithTransition("GameMenu");
    }

    // --- Кнопка "Back to Game" в GameMenu ---
    public void BackToGame()
    {
        LoadSceneWithTransition("Bedroom");
    }

    // --- Кнопка "Back to Main Menu" в GameMenu ---
    public void BackToMainMenu()
    {
        LoadSceneWithTransition("MainMenu");
    }

    public void OpenSet()
    {
        LoadSceneWithTransition("MainMenuSet");
    }

    // --- Приватная универсальная функция перехода ---
    private void LoadSceneWithTransition(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(sceneName);
    }

    // Добавляем метод для тестирования корутины
    public void Test_LoadSceneCoroutine(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    // Добавляем метод для проверки работы корутины
    public bool IsCoroutineRunning()
    {
        return GetComponent<MonoBehaviour>().IsInvoking();
    }
}
