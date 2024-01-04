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

public class JsonData
{
    public class UserName
    {
        public string data;
        public string message;
    }   

    public class Data
    {
        public DataWrapper data;
        public string message;
    }

    [System.Serializable]
    public class DataWrapper
    {
        public UserDataWrapper data;
    }

    [System.Serializable]
    public class UserDataWrapper
    {
        public List<UserTreeData> treeData;
        public string _id;
        public string email;
        public string username;
        public string password;
        public string lastAuthentication;
        public int __v;
    }
}

