using SimpleJSON;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class AuthenticationSystem : MonoBehaviour
{

    public GameObject loginPanel;
    public GameObject signinPanel;
    public GameObject loadingPage;

    [SerializeField]
    private GameObject appPage;

    #region Sign In Info
    string signEmail;
    string signUserName;
    string signPass;

    public void GetSignEmail(string text) => signEmail = text;
    public void GetSignUserName(string text) => signUserName = text;
    public void GetSignPass(string text) => signPass = text;

    #endregion

    public static AuthenticationSystem authentication;

    #region Log In Info
    string logUserNameOrEmail;
    string logPass;

    public void GetLogUserNameOrEmail(string text) => logUserNameOrEmail = text;
    public void GetLogPass(string text) => logPass = text;

    #endregion
    private static string url = "https://stem-backend-gvjp.onrender.com/";
    public static string GetUrl { get => url; }

    [System.Obsolete]
    private void OnEnable()
    {
        authentication = this;
        if (PlayerPrefs.GetString("Username") != "")
        {
            StartCoroutine(FinishLogIn(loginPanel));
        }
    }

    [System.Obsolete]
    public void LogIn()
    {
        StartCoroutine(RequestLogIn());
        IEnumerator RequestLogIn()
        {
            #region Json
            WWWForm json = new WWWForm();
            json.AddField("username", logUserNameOrEmail);
            json.AddField("password", logPass);
            #endregion

            using (UnityWebRequest www = UnityWebRequest.Post($"{url}account/Login", json))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    // Successfully retrieved data
                    Debug.Log("Error : " + www.error);
                }
                else
                {
                    JsonData.UserName data = JsonUtility.FromJson<JsonData.UserName>(www.downloadHandler.text);
                    PlayerPrefs.SetString("Username", data.data);
                    Debug.Log(data.data);
                    StartCoroutine(FinishLogIn(loginPanel));
                }
            }
        }
    }

    [System.Obsolete]
    public void SignIn()
    {
        StartCoroutine(RequestSignIn());

        IEnumerator RequestSignIn()
        {
            #region Json
            WWWForm json = new WWWForm();
            json.AddField("email", signEmail);
            json.AddField("username", signUserName);
            json.AddField("password", signPass);
            #endregion

            using (UnityWebRequest www = UnityWebRequest.Post($"{url}account/createAccount", json))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    // Successfully retrieved data
                    Debug.Log("Error : " + www.error);
                }
                else
                {
                    JsonData.UserName data = JsonUtility.FromJson<JsonData.UserName>(www.downloadHandler.text);
                    PlayerPrefs.SetString("Username", data.data);
                    StartCoroutine(FinishLogIn(signinPanel));
                }
            }
        }
    }

    [System.Obsolete]
    IEnumerator FinishLogIn(GameObject obj)
    {
        obj.SetActive(false);
        loadingPage.SetActive(true);
        Animator anim = loadingPage.GetComponent<Animator>();

        if (PlayerPrefs.GetString("Username") == null) StopCoroutine(FinishLogIn(obj));
        string username = PlayerPrefs.GetString("Username");
        Debug.Log(username);

        using (UnityWebRequest www = UnityWebRequest.Get($"{url}data/getData/{username}"))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                // Successfully retrieved data
                Debug.Log("Error : " + www.error);
            }
            else
            {
                string json = www.downloadHandler.text;

                yield return SaveSystem.Save.LoadData(json);
                appPage.SetActive(true);
                anim.SetBool("FnishLoad", true);
                yield return anim;
                loadingPage.SetActive(false);
            }
        }
    }

    public void Reset()
    {
        loginPanel.SetActive(true);
        signinPanel.SetActive(false);
        loadingPage.SetActive(false);

        loadingPage.GetComponent<Animator>().SetBool("FnishLoad", false);
    }
}
