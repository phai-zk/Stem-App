using UnityEngine.Events;
using UnityEngine;
using System.Text.RegularExpressions;
using TMPro;
using System;

public class TreeInfo : MonoBehaviour {
    public string treeName;
    [SerializeField]
    private TMP_Text showName;
    [SerializeField]
    private TMP_Text showMoisture;
    public Tree treeModel;
    [HideInInspector]
    public string moistureData;
    [HideInInspector]
    public string lightData;
    [HideInInspector]
    public string tempData;
    public UnityAction<TreeInfo> OnDescription;

    public void GetTreeModel(string name)
    {
        treeModel = MiddleData.Middle.FindTree(name);
    }
    
    public void SetUI(string _treeName, string _moistureData, string _lightData, string _tempData)
    {
        this.treeName = _treeName;
        this.moistureData = _moistureData;
        this.lightData = _lightData;
        this.tempData = _tempData;
    }

    [Obsolete]
    public void SetUp(Tree _tree) 
    {
        Match match = EnCodeData(this.moistureData);
        if (match == null) return;
        int delta = 0;
        if (match.Groups.Count > 1)
        {
            int num1 = Int32.Parse(match.Groups[1].Value);
            int num2 = Int32.Parse(match.Groups[2].Value);
            delta = num2-num1;
            this.showMoisture.text = $"{num2 - UnityEngine.Random.Range(-delta, delta)}%";
        }
        else if(match.Groups != null)
        {
            int num1 = Int32.Parse(match.Groups[1].Value);
            delta = num1;
            this.showMoisture.text = $"{num1 - UnityEngine.Random.Range(-delta, delta)}%";
        }

        this.showName.text = this.treeName;
        this.treeModel = _tree;
        this.treeModel.Preparing();
    }

    public void OnClick()
    {
        OnDescription?.Invoke(this);
    }
    
    public Match EnCodeData(string code)
    {
        // Define the regular expression pattern
        string pattern = @"(\d+)-(\d+)";
        // Create a Regex object
        Match match = Regex.Match(code, pattern);
        if (match.Success)
        {
            // Extract and print the captured numeric values
            return match;
        }
        else
        {
            pattern = @"(\d+)";
            Regex regex = new Regex(pattern);

            Match match2 = regex.Match(code);
            if (match2.Success)
            {
                return match2;
            }
            Debug.Log("Error : "+code);
            return null;
        }
    }

}