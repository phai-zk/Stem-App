using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingControl : MonoBehaviour
{
    [SerializeField]
    Page setting;
    Toggle[] toggles;
    SoundSetting[] sounds;

    private void Awake() {
        SettingToggle();
    }

    private void SettingToggle()
    {
        toggles = setting.GetComponentsInChildren<Toggle>();    
        foreach(var toggle in toggles)
        {
            toggle.SetUp();
        }
    }

}
