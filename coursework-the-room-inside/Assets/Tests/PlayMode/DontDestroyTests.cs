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
        // ������� ����������� ������ ����� ������ ������
        DontDestroy.instance = null;
    }

    [TearDown]
    public void Teardown()
    {
        // ���������� ��� ������� ����� �����
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
        // ������� ������ - Start() ��������� �������������
        testObject = new GameObject("TestObject");
        testObject.AddComponent<DontDestroy>();

        // ���� ���� ���� ����� Start() ����������
        yield return null;

        Assert.IsNotNull(DontDestroy.instance, "��������� ������ ���� ������");
    }

    [UnityTest]
    public IEnumerator ShouldDestroyDuplicateInstances()
    {
        // ������ ������
        var obj1 = new GameObject("Obj1");
        obj1.AddComponent<DontDestroy>();
        yield return null;

        // ������ ������ (������ ���� ���������)
        var obj2 = new GameObject("Obj2");
        obj2.AddComponent<DontDestroy>();
        yield return null;

        Assert.IsTrue(obj2 == null, "�������� ������ ���� ���������");
        Assert.AreEqual(obj1, DontDestroy.instance.gameObject, "������ �������� ������ ������");
    }
}