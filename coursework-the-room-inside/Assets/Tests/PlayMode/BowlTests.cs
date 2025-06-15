using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class BowlTests
{
    private GameObject bowlObject;
    private Bowl bowl;
    private Image bowlImage;
    private GameObject testItem;

    [SetUp]
    public void SetUp()
    {
        // Создаем миску
        bowlObject = new GameObject("Bowl");
        bowlImage = bowlObject.AddComponent<Image>();
        bowl = bowlObject.AddComponent<Bowl>();
        bowl.filledSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

        // Тестовый предмет для перетаскивания
        testItem = new GameObject("TestItem");
        testItem.AddComponent<Image>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(bowlObject);
        Object.DestroyImmediate(testItem);
    }

    // --- Тесты для миски с водой ---
    [UnityTest]
    public IEnumerator TryFill_WaterBowl_AcceptsFilledGlass()
    {
        // Arrange
        bowl.isWaterBowl = true;
        testItem.tag = "FilledGlass";

        // Act
        bowl.TryFill(testItem);

        // Assert
        Assert.IsTrue(bowl.isFilled);
        Assert.AreEqual(bowl.filledSprite, bowlImage.sprite);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TryFill_WaterBowl_RejectsFoodItem()
    {
        bowl.isWaterBowl = true;
        testItem.tag = "FoodItem";

        bowl.TryFill(testItem);

        Assert.IsFalse(bowl.isFilled);
        yield return null;
    }

    // --- Тесты для миски с едой ---
    [UnityTest]
    public IEnumerator TryFill_FoodBowl_AcceptsFoodItem()
    {
        bowl.isWaterBowl = false;
        testItem.tag = "FoodItem";

        bowl.TryFill(testItem);

        Assert.IsTrue(bowl.isFilled);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TryFill_FoodBowl_RejectsFilledGlass()
    {
        bowl.isWaterBowl = false;
        testItem.tag = "FilledGlass";

        bowl.TryFill(testItem);

        Assert.IsFalse(bowl.isFilled);
        yield return null;
    }

    // --- Общие тесты ---
    [UnityTest]
    public IEnumerator TryFill_DoesNothingIfAlreadyFilled()
    {
        bowl.isFilled = true;
        var originalSprite = bowlImage.sprite;

        bowl.TryFill(testItem);

        Assert.AreEqual(originalSprite, bowlImage.sprite);
        yield return null;
    }

    [UnityTest]
    public IEnumerator TryFill_DestroysItemOnSuccess()
    {
        // Arrange
        bowl.isWaterBowl = true;
        testItem.tag = "FilledGlass";

        // Act
        bowl.TryFill(testItem);

        // Ждём один кадр, чтобы Destroy() выполнился
        yield return null;

        // Assert
        Assert.IsTrue(testItem == null, "Предмет должен быть уничтожен после TryFill");

        // Альтернативная проверка (более надёжная)
        var itemStillExists = GameObject.Find("TestItem") != null;
        Assert.IsFalse(itemStillExists, "Объект TestItem всё ещё существует на сцене");
    }
}
