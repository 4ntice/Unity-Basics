using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundValue : MonoBehaviour
{
    public float currentVolume;
    public string soundName;

    public Slider soundSlider;
    public TMP_InputField soundInputField;

    public AudioMixer audioMixer;

    private void Start()
    {
        SetVolume(soundName, currentVolume);
#if UNITY_EDITOR
        SetVolumeBySlider();
#endif
    }

    const int subStringLength = 3;//Limit float value for input field string to ex. 0,3 => 3 letters
    public void SetVolumeBySlider()
    {
        float volume = soundSlider.value;
        SetVolume(soundName, volume);

        //Change Value of InputField
        string subString = volume.ToString(); 
        if (volume.ToString().Length > subStringLength)
        {
            subString = volume.ToString().Substring(0, subStringLength);
        }
        else
        {
            subString = volume.ToString();
        }
        soundInputField.text = subString;
    }

    public void SetVolumeByInputField()
    {
        float volume = float.Parse(soundInputField.text);
        SetVolume(soundName, volume);

        soundSlider.value = volume;
    }

    void SubmitName()
    {

    }

    public float SetVolume(string name, float volume)
    {
        audioMixer.SetFloat("master", volume);
        return currentVolume = volume;
    }
}
