using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    [SerializeField] Slider masterSlider;
    [SerializeField] AudioMixer masterMixer;
    // Start is called before the first frame update
    void Start()
    {
        SetVolume(PlayerPrefs.GetFloat("SavedMasterVolume", 100));
    }

    public void SetVolume(float volume)
    {
        if (volume < 1)
        {
            volume = .001f;
        }
        RefreshSlider(volume);
        PlayerPrefs.SetFloat("SavedMasterVolume", volume);
        masterMixer.SetFloat("MasterVolume", Mathf.Log10(volume / 100) * 20f);
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(masterSlider.value);
    }

    public void RefreshSlider(float volume)
    {
        masterSlider.value = volume;
    }
}
