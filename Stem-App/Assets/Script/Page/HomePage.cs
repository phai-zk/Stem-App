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

    [System.Obsolete]
    private void OnEnable()
    {
        Home = this;
        StartCoroutine(UpdateTemp());
        OnUpdateContentList -= UpdateList;
        OnUpdateContentList += UpdateList;
    }

    [System.Obsolete]
    protected override void Awake()
    {
        // PrepareUIContent();
        base.Awake();
    }

    [System.Obsolete]
    public void PrepareUIContent()
    {
        foreach (var tree in treeInfoBoxs)
        {
            UpdateList(tree);
        }
    }

    IEnumerator UpdateTemp()
    {
        int newTemp = 28 - UnityEngine.Random.Range(-2, 2);
        showTemp.text = $"{newTemp}.00";
        yield return new WaitForSeconds(5);
        StartCoroutine(UpdateTemp());
    }

    [System.Obsolete]
    public void UpdateList(TreeInfo tree)
    {
        TreeInfo treeInfo = Instantiate(treeInfoPrefab, contentBox).GetComponent<TreeInfo>();
        treeInfo.SetUI(tree.treeName, tree.moistureData, tree.lightData, tree.tempData);
        treeInfo.SetUp(MiddleData.Middle.GetTree());
        
        treeInfo.OnDescription += description.PrepareUI;
        
        treeInfoBoxs.Add(treeInfo);
    }
    public override void Reset() {
        foreach (var item in treeInfoBoxs)
        {
            Destroy(item.gameObject);
        }
        treeInfoBoxs.Clear();
    }

    public void OnSearch(string text) { }
}
