using System;
using TMPro;
using UnityEngine;

public class SettingPage : Page {
    [SerializeField]
    TMP_Text username;
    
    [System.Obsolete]
    protected override void Awake()
    {
        PrepareUI();
        base.Awake();
    }

    public void PrepareUI()
    {
        username.text = PlayerPrefs.GetString("Username");
    }

    [System.Obsolete]
    public void LogOut()
    {
        SaveSystem.Save.SaveData();
        PlayerPrefs.DeleteAll();
        Reset();
    }

    public override void Reset()
    {
        AuthenticationSystem.authentication.Reset();
        foreach (var item in MiddleData.Middle.allPage)
        {
            if (item.name == this.name) continue;
            item.Reset();
        }
        Navigator.ChangePage("HomePage");
    }

}