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
        // ������� �����
        bowlObject = new GameObject("Bowl");
        bowlImage = bowlObject.AddComponent<Image>();
        bowl = bowlObject.AddComponent<Bowl>();
        bowl.filledSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);

        // �������� ������� ��� ��������������
        testItem = new GameObject("TestItem");
        testItem.AddComponent<Image>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(bowlObject);
        Object.DestroyImmediate(testItem);
    }

    // --- ����� ��� ����� � ����� ---
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

    // --- ����� ��� ����� � ���� ---
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

    // --- ����� ����� ---
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

        // ��� ���� ����, ����� Destroy() ����������
        yield return null;

        // Assert
        Assert.IsTrue(testItem == null, "������� ������ ���� ��������� ����� TryFill");

        // �������������� �������� (����� �������)
        var itemStillExists = GameObject.Find("TestItem") != null;
        Assert.IsFalse(itemStillExists, "������ TestItem �� ��� ���������� �� �����");
    }
}
