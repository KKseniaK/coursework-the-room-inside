using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // URL Google Forms ��� ����� �������
    [SerializeField] private string feedbackUrl = "https://forms.gle/your-form-url";

    private void Update()
    {
        // ��������� ������� Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
            Debug.Log("Application.Quit() called");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Application.Quit() called");
    }

    public void OpenFeedbackForm()
    {
        Application.OpenURL(feedbackUrl);
        Debug.Log("FeedbackForm called");
    }
}

