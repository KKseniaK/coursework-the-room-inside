using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    // --- ������ "Start Game" � Main Menu ---
    public void StartGame()
    {
        LoadSceneWithTransition("Bedroom");
    }

    // --- ������ "Open Game Menu" � Bedroom ---
    public void OpenGameMenu()
    {
        LoadSceneWithTransition("GameMenu");
    }

    // --- ������ "Back to Game" � GameMenu ---
    public void BackToGame()
    {
        LoadSceneWithTransition("Bedroom");
    }

    // --- ������ "Back to Main Menu" � GameMenu ---
    public void BackToMainMenu()
    {
        LoadSceneWithTransition("MainMenu");
    }

    public void OpenSet()
    {
        LoadSceneWithTransition("MainMenuSet");
    }

    // --- ��������� ������������� ������� �������� ---
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
}
