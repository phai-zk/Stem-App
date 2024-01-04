using TMPro;
using UnityEngine;

public class CreatePage : Page {
    private string wifi; 
    [SerializeField]
    private GameObject wifiInputPage;
    private TreeInfo treeInfo;
    [SerializeField]
    private TMP_InputField input;  
    [SerializeField]
    private TMP_InputField treeNameInput;
    private string treeName = "";  
    [SerializeField]
    private Range moistureRange; 
    [SerializeField]
    private Range lightRange; 
    [SerializeField]
    private Range tempRange;

    public static CreatePage page;

    private void OnEnable() {
        page = this;
        wifiInputPage.SetActive(true);
        wifi = "";
    }

    protected override void Awake()
    {
        input.text = "";
        base.Awake();
    }

    public void FindWifi()  
    {
        if (wifi != null && wifi.Trim() != "")
        {
            wifiInputPage.SetActive(false);
            wifi = "";
        }
    }

    public void InputWifi(string info)
    {
        wifi = info;
    }

    public void InputTreeName(string info)  
    {
        treeName = info;
    }

    public void SubmitCreation()  
    {
        bool check0 = treeName.Trim() != "";
        bool check1 = moistureRange.dataOFRange != "";
        bool check2 = lightRange.dataOFRange != "";
        bool check3 = tempRange.dataOFRange != "";
        // Debug.Log($"{treeName} : {moistureRange.dataOFRange} : {lightRange.dataOFRange} : {tempRange.dataOFRange}");
        // Debug.Log($"Check : {check0} : {check1} : {check2} : {check3}");
        if (check0&&check1&&check2&&check3)
        {
            treeInfo = new TreeInfo();
            treeInfo.treeName = treeName;
            treeInfo.moistureData = moistureRange.dataOFRange;
            treeInfo.lightData = lightRange.dataOFRange;
            treeInfo.tempData = tempRange.dataOFRange;
            HomePage.OnUpdateContentList?.Invoke(treeInfo);
            Navigator.ChangePage(MiddleData.Middle.allPage[0].name);
            ResetData();
        }
    }

    public void ResetData()
    {
        treeNameInput.text = "";
        treeName = "";
        moistureRange.Reset();
        lightRange.Reset();
        tempRange.Reset();
    }
}