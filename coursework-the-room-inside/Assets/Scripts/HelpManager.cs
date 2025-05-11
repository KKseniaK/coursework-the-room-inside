using UnityEngine;
using System.Diagnostics;

//public class HelpManager : MonoBehaviour
//{
//    public string helpFilePath = "Assets/Help/Справка.chm"; // Путь к файлу справки

//    void Update()
//    {
//        // Обработка нажатия F1
//        if (Input.GetKeyDown(KeyCode.F1))
//        {
//            OpenHelpFile();
//        }
//    }

//    // Метод для кнопки "Справка"
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
//                Debug.LogError("Ошибка открытия справки: " + e.Message);
//                // Альтернативный вариант для WebGL
//                Application.OpenURL("https://ваш-сайт/справка");
//            }
//        }
//        else
//        {
//            Debug.LogWarning("Файл справки не найден по пути: " + fullPath);
//            // Можно показать встроенное окно с текстом
//            FindObjectOfType<UIManager>().ShowHelpPopup();
//        }
//    }
//}