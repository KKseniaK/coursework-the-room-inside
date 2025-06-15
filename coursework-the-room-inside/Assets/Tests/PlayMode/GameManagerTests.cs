using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

public class GameManagerTests
{
    private GameManager gameManager;
    private GameObject gameManagerObj;

    [SetUp]
    public void Setup()
    {
        gameManagerObj = new GameObject("GameManager");
        gameManager = gameManagerObj.AddComponent<GameManager>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameManagerObj);
    }

    [Test]
    public void QuitGame_LogsCorrectMessage()
    {
        // Arrange
        var logger = new TestLogger();
        gameManager.QuitGame();

        // Assert
        LogAssert.Expect(LogType.Log, "Application.Quit() called");
    }

    [Test]
    public void OpenFeedbackForm_LogsCorrectMessage()
    {
        // Arrange
        var logger = new TestLogger();
        gameManager.OpenFeedbackForm();

        // Assert
        LogAssert.Expect(LogType.Log, "FeedbackForm called");
    }

    // Мок для тестирования Input
    private class InputMock
    {
        private KeyCode? keyToSimulate;

        public void SetKeyDown(KeyCode key)
        {
            keyToSimulate = key;
        }

        public bool GetKeyDown(KeyCode key)
        {
            return keyToSimulate == key;
        }
    }

    // Помощник для отслеживания логов
    private class TestLogger : MonoBehaviour
    {
        public void Log(string message)
        {
            Debug.Log(message);
        }
    }
}