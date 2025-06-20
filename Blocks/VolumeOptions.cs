using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Windows;

public class VolumeOptions : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", ScaleValue(volume, 0.0f, 1.0f, -80.0f, 20.0f));
    }

    public void SetVideoVolume(float volume)
    {
        audioMixer.SetFloat("VideoVolume", ScaleValue(volume, 0.0f, 1.0f, -80.0f, 20.0f));
    }

    float ScaleValue(float inputScale, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (inputScale - inputMin) / (inputMax - inputMin) * (outputMax - outputMin) + outputMin;
    }
}
