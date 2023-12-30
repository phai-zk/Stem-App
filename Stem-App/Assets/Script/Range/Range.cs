using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Range : MonoBehaviour
{

    [SerializeField]
    private int maxValue;

    [SerializeField]
    private GameObject rangeIcon;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private Gradient color;

    [SerializeField]
    private List<RangeBTN> allRangeBTN;
    #region Get
    public List<RangeBTN> GetAllRangeBTN
    {
        get=> allRangeBTN;
    }
    #endregion

    [SerializeField]
    private RangeBTN rangeBTN;

    [SerializeField]
    private List<string> dataPerLevels;

    [SerializeField]
    private TMP_Text dataText;

    [HideInInspector]
    public string dataOFRange = null;

    private void Awake() {
        PrepareUI();
        OnRange(0);
    }

    private void PrepareUI()
    {
        if (allRangeBTN!=null)
        {
            foreach (var range in allRangeBTN)
            {
                Destroy(range.button.gameObject);
            }
        }

        for (int i = 0; i < maxValue; i++)
        {
            RangeBTN range = Instantiate(rangeIcon,content).AddComponent<RangeBTN>();
            Button button = range.GetComponent<Button>(); 
            range.SetData(
                range : this,
                btn : button,
                _value : i+1,
                _color : color.Evaluate((float)(i+1)/maxValue )
            );
            allRangeBTN.Add(range);
        }
    }

    public void OnRange(int value)
    {
        if (allRangeBTN == null) return;
        for (int i = 0; i <= maxValue; i++)
        {
            if (i-1 < 0) continue;
            allRangeBTN[i-1].UnActiveColor();
            if (i-1 < value)
            {
                allRangeBTN[i-1].ActiveColor();
            }
        }
        
        dataOFRange = GetValue(value);
        dataText.text = GetValue(value);
    }

    public string GetValue(int value)
    {
        string data;
        switch (value)
        {
            case 0 :
                data = "";
                break;
            case 1 : 
                data = dataPerLevels[0];
                break;
            case 2 : 
                data = dataPerLevels[1];
                break;
            case 3 : 
                data = dataPerLevels[2];
                break;
            case 4 : 
                data = dataPerLevels[3];
                break;
            case 5 : 
                data = dataPerLevels[4];
                break;
            default: 
                data = "";
                break;
        }
        return data;
    }

    public void Reset()
    {
        OnRange(0);
    }
}

public class RangeBTN : MonoBehaviour
{
    Range rangeType;
    public Button button;
    public int value;
    public Color color;

    public void SetData(Range range,Button btn, int _value, Color _color)
    {
        rangeType = range;
        button = btn;
        value = _value;
        color = _color;
        // Debug.Log($"{value} {value/5f} : {range} : {button} : {color}");
        button.onClick.AddListener(()=>OnClick());
    }

    public void OnClick()
    {
        rangeType.OnRange(value);
    }

    public void ActiveColor()
    {
        button.gameObject.GetComponent<Image>().color = color;
    }

    public void UnActiveColor()
    {
        button.gameObject.GetComponent<Image>().color = MiddleData.Middle.unActiveBTNColor;
    }
}