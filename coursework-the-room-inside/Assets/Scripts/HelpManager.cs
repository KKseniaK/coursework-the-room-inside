using UnityEngine;
using System.Diagnostics;
using System.IO;

public class HelpManager : MonoBehaviour
{
    private string manualPath;

    void Start()
    {
        // Формируем путь к файлу справки
        manualPath = Path.Combine(Application.streamingAssetsPath, "Справка.chm");
        UnityEngine.Debug.Log($"[HelpManager] Путь к справке: {manualPath}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UnityEngine.Debug.Log("[HelpManager] Нажата клавиша F1. Попытка открыть руководство...");
            OpenManual();
        }
    }

    public void OpenManual()
    {
        if (File.Exists(manualPath))
        {
            try
            {
                Process.Start(manualPath);
                UnityEngine.Debug.Log("[HelpManager] Руководство успешно запущено.");
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"[HelpManager] Ошибка при запуске файла справки: {e.Message}");
            }
        }
        else
        {
            UnityEngine.Debug.LogError($"[HelpManager] Руководство не найдено по пути: {manualPath}");
        }
    }
}
