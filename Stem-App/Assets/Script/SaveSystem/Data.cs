using System;
using System.Collections.Generic;
using TreeEditor;

[System.Serializable]
public class Data
{
    public List<UserTreeData> treeDatas = new List<UserTreeData>();
    public string userName;
}

[System.Serializable]
public class UserTreeData
{
    public string treeName;
    public string treeModel;
    public string moistureData;
    public string lightData;
    public string tempData;
}

[System.Serializable]
public class TreeDataListWrapper
{
    public List<JsonData.TreeData> treeData;
    public string message;
}


[System.Serializable]
public class JsonData
{
    public class UserName
    {
        public string data;
        public string message;
    }

    [System.Serializable]
    public class TreeData
    {
        public string treeName { get; set; }
        public string treeModel { get; set; }
        public string moistureData { get; set; }
        public string lightData { get; set; }
        public string tempData { get; set; }
    }

}

