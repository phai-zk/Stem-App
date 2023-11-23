using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoginData : MonoBehaviour
{
    Login data;
    TMP_Text errorTxt;

    private void Awake()
    {
        data = new Login();
    }

    public void Username(string info)
    {
        data.username = info;
    }

    public void Password(string info)
    {
        data.password = info;
    }

    public void Submit()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", data.username);
        form.AddField("password", data.password);
        StartCoroutine(API_Post("/account/Login", form));
    }

    IEnumerator API_Post(string api, WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:13756" + api, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Successfully fetched data
                if (!(www.downloadHandler.text).ToString().Contains("Error:"))
                {
                    PostData data = JsonUtility.FromJson<PostData>(www.downloadHandler.text);
                    Debug.Log(data.message);
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                }
            }
        }
    }
}
