using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class HomePage : Page
{
    [SerializeField]
    private TMP_Text showTemp;
    public static HomePage Home;

    [SerializeField]
    private TMP_InputField searchBar;
    private string searchInput;

    [SerializeField]
    private Transform contentBox;

    [SerializeField]
    private GameObject treeInfoPrefab;
    public List<TreeInfo> treeInfoBoxs;
    // public List<GameObject> objBoxs;
    public static UnityAction<TreeInfo> OnUpdateContentList;
    public Description description;

    private void OnEnable()
    {
        Home = this;
        StartCoroutine(UpdateTemp());
        OnUpdateContentList -= UpdateList;
        OnUpdateContentList += UpdateList;
    }

    protected override void Awake()
    {
        PrepareUIContent();
        base.Awake();
    }

    public void PrepareUIContent()
    {
        foreach (var tree in treeInfoBoxs)
        {
            UpdateList(tree);
        }
    }

    IEnumerator UpdateTemp()
    {
        int newTemp = 32 - UnityEngine.Random.Range(-2, 2);
        showTemp.text = $"{newTemp}.00";
        yield return new WaitForSeconds(5);
        StartCoroutine(UpdateTemp());
    }

    private void UpdateList(TreeInfo tree)
    {
        TreeInfo treeInfo = Instantiate(treeInfoPrefab, contentBox).GetComponent<TreeInfo>();

        treeInfo.SetUI(tree.treeName, tree.moistureData, tree.lightData, tree.tempData);
        treeInfo.SetUp(MiddleData.Middle.GetTree());
        
        treeInfo.OnDescription += description.PrepareUI;
        
        treeInfoBoxs.Add(treeInfo);
    }

    public void OnSearch(string text) { }
}
