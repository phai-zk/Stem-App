using System;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour {

    private Data data;

    private void OnEnable() {
        LoadData();
    }

    private void SaveData()
    {
        data = new Data();
        Page[] pages = MiddleData.Middle.allPage;
        HomePage home = pages[0].GetComponent<HomePage>();
        List<TreeInfo> treeInfo = home.treeInfoBoxs;
        for (var i = 0; i < treeInfo.Count; i++)
        {
            data.treeDatas[i].treeName = treeInfo[i].treeName;
            data.treeDatas[i].treeModel = treeInfo[i].treeModel.name;
            data.treeDatas[i].moistureData = treeInfo[i].moistureData;
            data.treeDatas[i].lightData = treeInfo[i].lightData;
            data.treeDatas[i].tempData = treeInfo[i].tempData;
        }
        Toggle[] toggles = pages[2].GetComponentsInChildren<Toggle>();    

        data.noti = toggles[0].isOn;
        data.bgm = toggles[1].isOn;
        data.plant = toggles[2].isOn;

        string json = JsonUtility.ToJson(data);
    }
    
    private void LoadData()
    {
                
    }
}