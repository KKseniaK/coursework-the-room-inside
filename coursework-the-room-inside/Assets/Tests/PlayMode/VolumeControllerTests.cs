using NUnit.Framework;
using System.IO;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class VolumeControllerTests
{
    [Test]
    public void VolumeChanged_UpdatesCorrectly()
    {
        var controller = new GameObject().AddComponent<VolumeController>();
        controller.slider = new GameObject().AddComponent<Slider>();

        // Симулируем изменение слайдера
        controller.slider.value = 0.25f; // 
        controller.SendMessage("HandleSliderValueChanged", 0.25f);

        Assert.AreEqual(-12.0412f, controller.CurrentVolumeDB, 0.001f);
    }
}