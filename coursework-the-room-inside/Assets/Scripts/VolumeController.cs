using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class VolumeController : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider slider;

    private float volumeValue;
    private const float multiplier = 20f;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);
    }

    private void HandleSliderValueChanged(float value)
    {
        volumeValue = Mathf.Log10(value) * multiplier;
        mixer.SetFloat(volumeParameter, volumeValue);
    }

    void Start()
    {
        volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * multiplier);
        slider.value = Mathf.Pow(10, volumeValue/multiplier);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, volumeValue);
    }
}
