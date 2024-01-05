using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class MiddleData : MonoBehaviour {

    public static MiddleData Middle;
    public Color lightFontColor;
    public Color darkFontColor;
    public Color activeBTNColor;
    public Color unActiveBTNColor;
    public Tree[] trees;
    public  Page[] allPage;


    [Obsolete]
    private void OnEnable() {
        Middle = this;
        GetAllPage();
        GetAllTree();
    }

    [Obsolete]
    private void GetAllPage()
    {
        allPage = GameObject.FindObjectsOfType<Page>(true);
        Array.Reverse(allPage);
    }

    private void GetAllTree()
    {
        trees = Resources.LoadAll<Tree>("Tree");
    }

    public Tree GetTree()
    {
        return trees[UnityEngine.Random.Range(0,trees.Length-1)];
    }

    public Tree FindTree(string name)
    {
        foreach (var tree in trees)
        {
            if (tree.name == name)
            {
                Debug.LogError($"{tree.name} : {name}");
                return tree;
            }
        }
        return null;
    }
}