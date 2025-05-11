using UnityEngine;
using System.Diagnostics;
using System.IO;

public class HelpManager : MonoBehaviour
{
    private string manualPath;

    void Start()
    {
        // ���� � ����� � StreamingAssets
        manualPath = Path.Combine(Application.streamingAssetsPath, "�������.chm");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OpenManual();
        }
    }

    public void OpenManual()
    {
        if (File.Exists(manualPath))
        {
            Process.Start(manualPath);
        }
        else
        {
            UnityEngine.Debug.LogError("����������� �� ������� �� ����: " + manualPath);
        }
    }
}