
using UnityEngine;

public class AppTheme : MonoBehaviour {

    public static Color GetColor(Element element,Theme theme)
    {
        Color color = new Color();
        switch (theme)
        {
            case Theme.Light :
                color = element.lightColor;
                break;
            case Theme.Dark :
                color = element.darkColor;
                break;
            default :
                color = element.darkColor;
                break;
        }
        return color;
    }
}

public enum Theme
{
    Light,
    Dark
}
