using UnityEngine;
using System.Diagnostics;

//public class HelpManager : MonoBehaviour
//{
//    public string helpFilePath = "Assets/Help/�������.chm"; // ���� � ����� �������

//    void Update()
//    {
//        // ��������� ������� F1
//        if (Input.GetKeyDown(KeyCode.F1))
//        {
//            OpenHelpFile();
//        }
//    }

//    // ����� ��� ������ "�������"
//    public void OnHelpButtonClick()
//    {
//        OpenHelpFile();
//    }

//    private void OpenHelpFile()
//    {
//        string fullPath = System.IO.Path.Combine(Application.streamingAssetsPath, helpFilePath);

//        if (System.IO.File.Exists(fullPath))
//        {
//            try
//            {
//                Process.Start(fullPath);
//            }
//            catch (System.Exception e)
//            {
//                Debug.LogError("������ �������� �������: " + e.Message);
//                // �������������� ������� ��� WebGL
//                Application.OpenURL("https://���-����/�������");
//            }
//        }
//        else
//        {
//            Debug.LogWarning("���� ������� �� ������ �� ����: " + fullPath);
//            // ����� �������� ���������� ���� � �������
//            FindObjectOfType<UIManager>().ShowHelpPopup();
//        }
//    }
//}