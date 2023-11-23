using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIClient : MonoBehaviour
{
    string baseURL = "http://localhost:13756";

    public void Post(string api)
    {
        StartCoroutine(API_Post(api));
    }

    IEnumerator API_Post(string api)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(baseURL + api, new WWWForm()))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Successfully fetched data
                PostData data = JsonUtility.FromJson<PostData>(www.downloadHandler.text);
            }
        }
    }

    public void Post(string api, WWWForm form)
    {
        StartCoroutine(API_Post(api, form));
    }

    IEnumerator API_Post(string api, WWWForm form)
    {
        using (UnityWebRequest www = UnityWebRequest.Post(baseURL + api, form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(www.error);
            }
            else
            {
                // Successfully fetched data
                PostData data = JsonUtility.FromJson<PostData>(www.downloadHandler.text);
            }
        }
    }
}
