using UnityEngine;
using System.Diagnostics;
using System.IO;

public class HelpManager : MonoBehaviour
{
    private string manualPath;

    void Start()
    {
        // ��������� ���� � ����� �������
        manualPath = Path.Combine(Application.streamingAssetsPath, "�������.chm");
        UnityEngine.Debug.Log($"[HelpManager] ���� � �������: {manualPath}");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            UnityEngine.Debug.Log("[HelpManager] ������ ������� F1. ������� ������� �����������...");
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
                UnityEngine.Debug.Log("[HelpManager] ����������� ������� ��������.");
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogError($"[HelpManager] ������ ��� ������� ����� �������: {e.Message}");
            }
        }
        else
        {
            UnityEngine.Debug.LogError($"[HelpManager] ����������� �� ������� �� ����: {manualPath}");
        }
    }
}
