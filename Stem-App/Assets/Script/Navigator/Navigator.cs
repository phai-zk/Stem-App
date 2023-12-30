using UnityEngine;
using UnityEngine.UI;


public class Navigator : MonoBehaviour
{
    [SerializeField]
    private GameObject content;
    private static Button[] navBTNs;
    private Page[] pages;

    private void OnEnable() {
        pages = MiddleData.Middle.allPage;
        SetUI();
    }

    private void SetUI()
    {
        navBTNs = content.GetComponentsInChildren<Button>();
        for (var i = 0; i < pages.Length; i++)
        {
            NavButton newBTN = new NavButton(navBTNs[i]);
            newBTN.pageName = pages[i].name;
        }
    }

    public static void ChangePage(string pageName)
    {
        var allPage = MiddleData.Middle.allPage;
        for (int i = 0; i < allPage.Length; i++)
        {
            Image icon = navBTNs[i].transform.GetChild(0).GetComponent<Image>();
            
            icon.color = MiddleData.Middle.unActiveBTNColor;
            allPage[i].gameObject.SetActive(false); 

            if (allPage[i].name == pageName)
            {
                if (CreatePage.page != null) CreatePage.page.ResetData();
                allPage[i].gameObject.SetActive(true);
                icon.color = MiddleData.Middle.activeBTNColor;
                Page.OnChangePage?.Invoke(allPage[i]);
            }
        }
    }
}

public class NavButton
{
    public Button button;
    public string pageName;

    public NavButton(Button btn)
    {
        button = btn;
        button.onClick.AddListener(()=>
        {
            if (Page.GetCurrentPage.name == pageName) return;
            Navigator.ChangePage(pageName);
        });
    }
    
}
