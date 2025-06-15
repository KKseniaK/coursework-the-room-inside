using UnityEngine;
using UnityEngine.Audio;

public class VolumeInit : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;


    protected virtual void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, 0);
        mixer.SetFloat(volumeParameter, volumeValue);
    }
    
}
