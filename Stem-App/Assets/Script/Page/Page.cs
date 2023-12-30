using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Page : MonoBehaviour
{
    private static Page currentPage = null;
    public Element[] element; //not now
    public static UnityAction<Page> OnChangePage; //not now

    public static Page GetCurrentPage {
        get => currentPage;
        set => currentPage = value;
    }

    protected virtual void Awake() {
        OnChangePage += ChangeCurrentPage;
        currentPage = MiddleData.Middle.allPage[0];
        StartCoroutine(SceneSetup());
    }

    IEnumerator SceneSetup()
    {
        yield return null;
        Navigator.ChangePage(currentPage.name);
    }

    private void ChangeCurrentPage(Page page)
    {
        currentPage = page;
    }
}
