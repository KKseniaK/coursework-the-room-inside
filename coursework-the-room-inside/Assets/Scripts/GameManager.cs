using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private string feedbackUrl = "https://docs.google.com/forms/d/e/1FAIpQLScF7qLBIFw6d0m7RljGOZDRvtEHyQzYnEjRuMRp7tFDYc2_EA/viewform?usp=preview";

    private void Update()
    {
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