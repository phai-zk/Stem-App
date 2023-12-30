using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Element : MonoBehaviour
{
    [HideInInspector]
    public GameObject element;
    [HideInInspector]
    public Color lightColor;
    public Color darkColor;

    private void Awake() {
        element = this.gameObject;
        lightColor = this.GetComponent<Image>().color;
    }

    public void ChangeColor(Theme theme)
    {
        this.GetComponent<Image>().color = AppTheme.GetColor(this,theme);
    }
    
}
