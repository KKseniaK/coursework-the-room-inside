using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;

//public class BowlManagerTests
//{
//    private GameObject managerObj;
//    private BowlManager manager;
//    private Bowl foodBowl;
//    private Bowl waterBowl;

//    [SetUp]
//    public void Setup()
//    {
//        // ������� ��������
//        managerObj = new GameObject("Manager");
//        manager = managerObj.AddComponent<BowlManager>();

//        // ������� �����
//        foodBowl = new GameObject("FoodBowl").AddComponent<Bowl>();
//        waterBowl = new GameObject("WaterBowl").AddComponent<Bowl>();

//        // ��������� ������
//        manager.foodBowl = foodBowl;
//        manager.waterBowl = waterBowl;
//    }

//    [TearDown]
//    public void Teardown()
//    {
//        Object.DestroyImmediate(managerObj);
//        Object.DestroyImmediate(foodBowl.gameObject);
//        Object.DestroyImmediate(waterBowl.gameObject);
//    }

//    [UnityTest]
//    public IEnumerator CheckBowls_TriggersWhenBothFilled()
//    {
//        // ����������
//        foodBowl.isFilled = true;
//        waterBowl.isFilled = true;

//        // ��������
//        manager.CheckBowls();

//        // ��������
//        Assert.IsTrue(manager.animationPlayed);
//        LogAssert.Expect(LogType.Log, "��� ����� ���������! ��������� �������� �����...");
//        yield return null;
//    }

//    [UnityTest]
//    public IEnumerator CheckBowls_DoesNotTriggerWhenOneEmpty()
//    {
//        // ������� 1: ������ ���
//        foodBowl.isFilled = true;
//        manager.CheckBowls();
//        Assert.IsFalse(manager.animationPlayed);

//        // ������� 2: ������ ����
//        foodBowl.isFilled = false;
//        waterBowl.isFilled = true;
//        manager.CheckBowls();
//        Assert.IsFalse(manager.animationPlayed);

//        yield return null;
//    }

//    [UnityTest]
//    public IEnumerator CheckBowls_TriggersOnlyOnce()
//    {
//        // ������ ���������� (������ ���������)
//        foodBowl.isFilled = true;
//        waterBowl.isFilled = true;

//        // ������� ��������� � ���
//        LogAssert.Expect(LogType.Log, "��� ����� ���������! ��������� �������� �����...");
//        manager.CheckBowls();

//        // ��������� ��� ���� ����������
//        Assert.IsTrue(manager.animationPlayed, "���� animationPlayed ������ ���� true ����� ������� ����������");

//        // ���������� ������ ��������� ����� (���� ��������)
//        foodBowl.isFilled = false;
//        waterBowl.isFilled = false;

//        // ��������� ����� (�� ������ ������ ������)
//        manager.CheckBowls();

//        // ��������� ��� ����� ��������� ���
//        LogAssert.NoUnexpectedReceived();
//        yield return null;
//    }
//}