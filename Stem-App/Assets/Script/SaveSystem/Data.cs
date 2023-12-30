using System;
using System.Collections.Generic;

public class Data
{
    public List<UserTreeData> treeDatas = new List<UserTreeData>();
    public string userName;
    public bool noti;
    public bool bgm;
    public bool plant;    
}

public class UserTreeData
{
   public string treeName;
   public string treeModel;
   public string moistureData;
   public string lightData;
   public string tempData;
}
