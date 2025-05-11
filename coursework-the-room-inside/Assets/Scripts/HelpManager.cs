using UnityEngine;
using System.Diagnostics;
using System.IO;

public class HelpManager : MonoBehaviour
{
    private string manualPath;

    void Start()
    {
        // Путь к файлу в StreamingAssets
        manualPath = Path.Combine(Application.streamingAssetsPath, "Справка.chm");
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
            UnityEngine.Debug.LogError("Руководство не найдено по пути: " + manualPath);
        }
    }
}