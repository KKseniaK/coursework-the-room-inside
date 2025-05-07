using UnityEngine;

public class RoomRotation : MonoBehaviour
{
    public Canvas[] wallCanvases; // ������ Canvas ��� ����
    private int currentWallIndex = 0;

    private void Start()
    {
        ShowCurrentWall(); // �������� ��������� �����
    }

    public void RotateRight()
    {
        Debug.Log("������ ������!");
        currentWallIndex = (currentWallIndex + 1) % 4;
        Debug.Log($"����������� �� ����� {currentWallIndex}");
        ShowCurrentWall();
    }

    public void RotateLeft()
    {
        currentWallIndex = (currentWallIndex - 1 + 4) % 4;
        Debug.Log($"����������� �� ����� {currentWallIndex}");
        ShowCurrentWall();
    }

    private void ShowCurrentWall()
    {
        Debug.Log($"----- ������� �����: {currentWallIndex} -----");

        for (int i = 0; i < wallCanvases.Length; i++)
        {
            if (wallCanvases[i] == null)
            {
                Debug.LogError($"Canvas {i} �� ��������!");
                continue;
            }

            bool isActive = (i == currentWallIndex);
            wallCanvases[i].enabled = isActive;
            Debug.Log($"{wallCanvases[i].name} -> enabled: {isActive}");
        }
    }
}
