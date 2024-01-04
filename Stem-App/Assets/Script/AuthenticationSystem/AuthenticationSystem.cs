using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class AuthenticationSystem : MonoBehaviour {


    [SerializeField]
    private GameObject loginPanel;

    [SerializeField]
    private GameObject signinPanel;

    [SerializeField]
    private GameObject loadingPage;

    [SerializeField]
    private GameObject appPage;

    #region Sign In Info
    string signEmail;
    string signUserName;
    string signPass;

    public void GetSignEmail(string text) => signEmail = text;
    public void GetSignUserName( string text) => signUserName = text;
    public void GetSignPass( string text) => signPass = text;

    #endregion

    #region Log In Info
    string logUserNameOrEmail;
    string logPass;

    public void GetLogUserNameOrEmail(string text) => logUserNameOrEmail = text;
    public void GetLogPass(string text) => logPass = text;

    #endregion
    private static string url = "http://localhost:5000/";
    public static string GetUrl { get => url; }


    public void LogIn()
    {
        StartCoroutine(RequestLogIn());
        IEnumerator RequestLogIn()
        {
            #region Json
            WWWForm json = new WWWForm();
            json.AddField("username",logUserNameOrEmail);
            json.AddField("password",logPass);        
            #endregion
            
            using (UnityWebRequest www = UnityWebRequest.Post($"{url}account/Login",json))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    // Successfully retrieved data
                    Debug.Log("Error : "+www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text.ToString());
                    JsonData.UserName data = JsonUtility.FromJson<JsonData.UserName>(www.downloadHandler.text);
                    PlayerPrefs.SetString("Username", data.data);
                    StartCoroutine(FinishLogIn(loginPanel));    
                }
            }
        }
    }
    
    public void SignIn()
    {
        StartCoroutine(RequestSignIn());

        IEnumerator RequestSignIn()
        {
            #region Json
            WWWForm json = new WWWForm();
            json.AddField("email",signEmail);
            json.AddField("username",signUserName);
            json.AddField("password",signPass);        
            #endregion
            
            using (UnityWebRequest www = UnityWebRequest.Post($"{url}account/createAccount",json))
            {
                yield return www.SendWebRequest();

                if (www.result != UnityWebRequest.Result.Success)
                {
                    // Successfully retrieved data
                    Debug.Log("Error : "+www.error);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text.ToString());
                    JsonData.UserName data = JsonUtility.FromJson<JsonData.UserName>(www.downloadHandler.text);
                    PlayerPrefs.SetString("Username", data.data);
                    StartCoroutine(FinishLogIn(signinPanel));
                }   
            }
        }
    }

    IEnumerator FinishLogIn(GameObject obj)  
    {
        obj.SetActive(false);
        loadingPage.SetActive(true);
        Animator anim = loadingPage.GetComponent<Animator>();

        if (PlayerPrefs.GetString("Username") == null) StopCoroutine(FinishLogIn(obj));
        string username = PlayerPrefs.GetString("Username");

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
                Debug.Log(www.downloadHandler.text.ToString());
                JSONNode json = JSON.Parse(www.downloadHandler.text);
                List<UserTreeData> uTD = JsonUtility.FromJson<List<UserTreeData>>(json["Data"]["treeData"]);
                if (uTD == null)
                {
                    uTD = new List<UserTreeData>();
                }

                Data data = new Data
                { 
                    treeDatas = uTD,
                    userName = json["Data"]["username"]
                };

                // Debug.Log( " : " + json["Data"]["treeData"]);

                // if((data.treeDatas == null)) ;

                yield return SaveSystem.Save.LoadData(data);
                appPage.SetActive(true);
                anim.SetBool("FnishLoad", true);
                yield return anim;
                loadingPage.SetActive(false);
            }
        }
    }
}
