using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class DontDestroyTests
{
    private GameObject testObject;

    [SetUp]
    public void Setup()
    {
        // Очищаем статическую ссылку перед каждым тестом
        DontDestroy.instance = null;
    }

    [TearDown]
    public void Teardown()
    {
        // Уничтожаем все объекты после теста
        Object[] objects = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (Object obj in objects)
        {
            if (obj != null)
            {
                Object.DestroyImmediate(obj);
            }
        }
    }

    [UnityTest]
    public IEnumerator ShouldCreateSingleInstance()
    {
        // Создаем объект - Start() вызовется автоматически
        testObject = new GameObject("TestObject");
        testObject.AddComponent<DontDestroy>();

        // Ждем один кадр чтобы Start() выполнился
        yield return null;

        Assert.IsNotNull(DontDestroy.instance, "Экземпляр должен быть создан");
    }

    [UnityTest]
    public IEnumerator ShouldDestroyDuplicateInstances()
    {
        // Первый объект
        var obj1 = new GameObject("Obj1");
        obj1.AddComponent<DontDestroy>();
        yield return null;

        // Второй объект (должен быть уничтожен)
        var obj2 = new GameObject("Obj2");
        obj2.AddComponent<DontDestroy>();
        yield return null;

        Assert.IsTrue(obj2 == null, "Дубликат должен быть уничтожен");
        Assert.AreEqual(obj1, DontDestroy.instance.gameObject, "Должен остаться первый объект");
    }
}