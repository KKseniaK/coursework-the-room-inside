using NUnit.Framework;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GlassTest
{
    private GameObject glassObject;
    private Glass glass;
    private Image glassImage;

    [SetUp]
    public void SetUp()
    {
        // Создаем объект стакана
        glassObject = new GameObject("Glass");
        glassImage = glassObject.AddComponent<Image>();
        glass = glassObject.AddComponent<Glass>();

        // Задаем спрайт для наполненного стакана (можно использовать `Resources.Load` или мок)
        glass.filledSprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 1, 1), Vector2.zero);
    }

    [TearDown]
    public void TearDown()
    {
        // Уничтожаем объект после каждого теста
        Object.DestroyImmediate(glassObject);
    }

    [UnityTest]
    public IEnumerator FillWithWater_SetsIsFilledToTrue()
    {
        // Act
        glass.FillWithWater();

        // Assert
        Assert.IsTrue(glass.isFilled);
        yield return null; // Play Mode тесты должны возвращать IEnumerator
    }

    [UnityTest]
    public IEnumerator FillWithWater_ChangesSprite()
    {
        // Act
        glass.FillWithWater();

        // Assert
        Assert.AreEqual(glass.filledSprite, glassImage.sprite);
        yield return null;
    }

    [UnityTest]
    public IEnumerator FillWithWater_SetsTagToFilledGlass()
    {
        // Act
        glass.FillWithWater();

        // Assert
        Assert.AreEqual("FilledGlass", glassObject.tag);
        yield return null;
    }

    [UnityTest]
    public IEnumerator FillWithWater_DoesNothingIfAlreadyFilled()
    {
        // Arrange
        glass.isFilled = true;
        var originalSprite = glassImage.sprite;

        // Act
        glass.FillWithWater();

        // Assert
        Assert.AreEqual(originalSprite, glassImage.sprite); // Спрайт не изменился
        yield return null;
    }
}