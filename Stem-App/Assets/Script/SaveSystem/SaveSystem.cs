using System;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;
using UnityEngine.Networking;

public class SaveSystem : MonoBehaviour
{

    private Data data;
    public static SaveSystem Save;

    private void OnEnable()
    {
        Save = this;
    }

    [Obsolete]
    public void SaveData()
    {
        if (PlayerPrefs.GetString("Username") == null) return;
        data = new Data();
        Page[] pages = MiddleData.Middle.allPage;
        HomePage home = pages[0].GetComponent<HomePage>();
        List<TreeInfo> treeInfo = home.treeInfoBoxs;
        // Debug.Log(treeInfo.Count);

        for (var i = 0; i < treeInfo.Count; i++)
        {
            UserTreeData userTree = new UserTreeData();
            userTree.treeName = treeInfo[i].treeName;
            userTree.treeModel = treeInfo[i].treeModel.name;
            userTree.moistureData = treeInfo[i].moistureData;
            userTree.lightData = treeInfo[i].lightData;
            userTree.tempData = treeInfo[i].tempData;

            data.treeDatas.Add(userTree);
            // Debug.Log(data.treeDatas[i].treeName);
        }
        data.userName = PlayerPrefs.GetString("Username");

        WWWForm form = new WWWForm();
        form.AddField("rData", JsonUtility.ToJson(data));
        StartCoroutine(SaveGame(form));
    }

    [Obsolete]
    IEnumerator SaveGame(WWWForm json)
    {
        string username = PlayerPrefs.GetString("Username");
        if (username == null) StopCoroutine(SaveGame(null));

        using (UnityWebRequest www = UnityWebRequest.Post($"{AuthenticationSystem.GetUrl}data/SaveData/{username}", json))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Successfully retrieved data
                Debug.LogError("Error : " + www.error);
            }
        }
    }

    [Obsolete]
    public Coroutine LoadData(string jsonData)
    {
        JSONNode json = JSON.Parse(jsonData);

        // Access the array of tree data
        JSONArray treeDataArray = json["treeData"].AsArray;

        SettingPage setting = MiddleData.Middle.allPage[2].GetComponent<SettingPage>();
        HomePage home = MiddleData.Middle.allPage[0].GetComponent<HomePage>();
        setting.PrepareUI();
        home.treeInfoBoxs = new List<TreeInfo>();
        // Loop through each tree data
        foreach (JSONNode treeDataNode in treeDataArray)
        {
            // Access values for each tree data
            TreeInfo info = new TreeInfo{
                treeName = treeDataNode["treeName"],
                treeModel = MiddleData.Middle.FindTree(treeDataNode["treeModel"]),
                moistureData = treeDataNode["moistureData"],
                lightData = treeDataNode["lightData"],
                tempData = treeDataNode["tempData"],
            };
            Debug.LogError(treeDataNode["treeName"] + " : "+treeDataNode["treeModel"]);

            // Print values for each tree data
            // Debug.Log($"{treeDataNode["treeName"]}, {treeDataNode["treeModel"]}, {treeDataNode["moistureData"]}");
            // Debug.Log($"{info.treeName}, {info.moistureData}, {info.lightData}, {info.tempData}");
            home.UpdateList(info);
        }

        return null;
    }

    [Obsolete]
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }

}