using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphicSettings : MonoBehaviour
{
    public Dropdown windowOptionDropdown;

    private void Start()
    {
        foreach(Enum e in Enum.GetValues(typeof(FullScreenMode)))
        {
            Debug.Log(e.ToString());
        }
    }

    //public enum FullScreenMode
    //{
    //    ExclusiveFullScreen,
    //    FullScreenWindow,
    //    MaximizedWindow,
    //    Windowed
    //}

    public void WindowModes()
    {
        Screen.fullScreenMode = FullScreenMode.Windowed;
    }
}
