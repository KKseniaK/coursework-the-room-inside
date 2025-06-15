using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.TestTools;

public class VolumeInitTests
{
    private GameObject testObject;
    private TestableVolumeInit volumeInit;
    private AudioMixer mixer;

    private const string VolumeParameter = "MasterVolume";

    public class TestableVolumeInit : VolumeInit
    {
        public void CallStartManually() => Start();
    }

    [UnitySetUp]
    public IEnumerator SetUp()
    {
        mixer = Resources.Load<AudioMixer>("Mixers/MasterMixer"); // Ensure correct path and name

        Assert.IsNotNull(mixer, "AudioMixer �� ������. �������, ��� �� ����� � Resources/Mixers/MasterMixer.mixer");

        testObject = new GameObject("VolumeInitTestObject");
        volumeInit = testObject.AddComponent<TestableVolumeInit>();
        volumeInit.mixer = mixer;
        volumeInit.volumeParameter = VolumeParameter;

        yield return null;
    }

    [UnityTest]
    public IEnumerator VolumeIsSetFromPlayerPrefs()
    {
        float testVolume = -12.5f;
        PlayerPrefs.SetFloat(VolumeParameter, testVolume);

        volumeInit.CallStartManually(); // ����� Start �������

        float actualVolume;
        bool found = mixer.GetFloat(VolumeParameter, out actualVolume);

        Assert.IsTrue(found, "�������� ��������� �� ������ � AudioMixer");
        Assert.AreEqual(testVolume, actualVolume, 0.1f);

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        PlayerPrefs.DeleteKey(VolumeParameter);
        Object.Destroy(testObject);
        yield return null;
    }
}
