using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SaveSystem : MonoBehaviour {

    private Data data;
    public static SaveSystem Save;

    private void OnEnable() {
        Save = this;
    }

    [Obsolete]
    private void SaveData()
    {
        if (PlayerPrefs.GetString("Username") == null) return;
        data = new Data();
        Page[] pages = MiddleData.Middle.allPage;
        HomePage home = pages[0].GetComponent<HomePage>();
        List<TreeInfo> treeInfo = home.treeInfoBoxs;
        Debug.Log(treeInfo.Count);

        for (var i = 0; i < treeInfo.Count; i++)
        {
            UserTreeData userTree = new UserTreeData();
            userTree.treeName = treeInfo[i].treeName;
            userTree.treeModel = treeInfo[i].treeModel.name;
            userTree.moistureData = treeInfo[i].moistureData;
            userTree.lightData = treeInfo[i].lightData;
            userTree.tempData = treeInfo[i].tempData;

            data.treeDatas.Add(userTree);
            Debug.Log(data.treeDatas[i].treeName);
        }
        data.userName = PlayerPrefs.GetString("Username");

        WWWForm form = new WWWForm();
        form.AddField("rData",JsonUtility.ToJson(data));
        StartCoroutine(SaveGame(form));
    }

    [Obsolete]
    IEnumerator SaveGame(WWWForm json) 
    {
        string username = PlayerPrefs.GetString("Username");

        using (UnityWebRequest www = UnityWebRequest.Post($"{AuthenticationSystem.GetUrl}data/SaveData/{username}", json))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Successfully retrieved data
                Debug.Log("Error : " + www.error);
            }
            else
            {
                Debug.Log(www.downloadHandler.text.ToString());
            }
        }
    }
    
    public Coroutine LoadData(Data _data)
    {
        data = _data;

        Page[] pages = MiddleData.Middle.allPage;
        HomePage home = pages[0].GetComponent<HomePage>();
        List<TreeInfo> treeInfo = home.treeInfoBoxs;
        
        for (int i = 0; i < data.treeDatas.Count; i++)
        {
            treeInfo[i].treeName = data.treeDatas[i].treeName;
            treeInfo[i].treeModel.name = data.treeDatas[i].treeModel;
            treeInfo[i].moistureData = data.treeDatas[i].moistureData;
            treeInfo[i].lightData = data.treeDatas[i].lightData;
            treeInfo[i].tempData = data.treeDatas[i].tempData;
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

    [Obsolete]
    private void OnApplicationQuit()
    {
        SaveData();
    }


}