using UnityEngine;

public class RoomRotation : MonoBehaviour
{
    public Canvas[] wallCanvases; // Массив Canvas для стен
    private int currentWallIndex = 0;

    private void Start()
    {
        ShowCurrentWall(); // Включить начальную стену
    }

    public void RotateRight()
    {
        Debug.Log("Кнопка нажата!");
        currentWallIndex = (currentWallIndex + 1) % 4;
        Debug.Log($"Переключено на стену {currentWallIndex}");
        ShowCurrentWall();
    }

    public void RotateLeft()
    {
        currentWallIndex = (currentWallIndex - 1 + 4) % 4;
        Debug.Log($"Переключено на стену {currentWallIndex}");
        ShowCurrentWall();
    }

    private void ShowCurrentWall()
    {
        Debug.Log($"----- Текущая стена: {currentWallIndex} -----");

        for (int i = 0; i < wallCanvases.Length; i++)
        {
            if (wallCanvases[i] == null)
            {
                Debug.LogError($"Canvas {i} не назначен!");
                continue;
            }

            bool isActive = (i == currentWallIndex);
            wallCanvases[i].enabled = isActive;
            Debug.Log($"{wallCanvases[i].name} -> enabled: {isActive}");
        }
    }
}
