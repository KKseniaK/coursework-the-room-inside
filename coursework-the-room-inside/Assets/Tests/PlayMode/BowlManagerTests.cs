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
//        // Создаем менеджер
//        managerObj = new GameObject("Manager");
//        manager = managerObj.AddComponent<BowlManager>();

//        // Создаем миски
//        foodBowl = new GameObject("FoodBowl").AddComponent<Bowl>();
//        waterBowl = new GameObject("WaterBowl").AddComponent<Bowl>();

//        // Связываем ссылки
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
//        // Подготовка
//        foodBowl.isFilled = true;
//        waterBowl.isFilled = true;

//        // Действие
//        manager.CheckBowls();

//        // Проверки
//        Assert.IsTrue(manager.animationPlayed);
//        LogAssert.Expect(LogType.Log, "Обе миски заполнены! Запускаем анимацию лапок...");
//        yield return null;
//    }

//    [UnityTest]
//    public IEnumerator CheckBowls_DoesNotTriggerWhenOneEmpty()
//    {
//        // Вариант 1: Только еда
//        foodBowl.isFilled = true;
//        manager.CheckBowls();
//        Assert.IsFalse(manager.animationPlayed);

//        // Вариант 2: Только вода
//        foodBowl.isFilled = false;
//        waterBowl.isFilled = true;
//        manager.CheckBowls();
//        Assert.IsFalse(manager.animationPlayed);

//        yield return null;
//    }

//    [UnityTest]
//    public IEnumerator CheckBowls_TriggersOnlyOnce()
//    {
//        // Первое заполнение (должно сработать)
//        foodBowl.isFilled = true;
//        waterBowl.isFilled = true;

//        // Ожидаем сообщение в лог
//        LogAssert.Expect(LogType.Log, "Обе миски заполнены! Запускаем анимацию лапок...");
//        manager.CheckBowls();

//        // Проверяем что флаг установлен
//        Assert.IsTrue(manager.animationPlayed, "Флаг animationPlayed должен быть true после первого заполнения");

//        // Сбрасываем только состояние мисок (флаг остается)
//        foodBowl.isFilled = false;
//        waterBowl.isFilled = false;

//        // Повторный вызов (не должен ничего делать)
//        manager.CheckBowls();

//        // Проверяем что новых сообщений нет
//        LogAssert.NoUnexpectedReceived();
//        yield return null;
//    }
//}