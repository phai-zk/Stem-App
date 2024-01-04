using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Tree", menuName = "Tree/model", order = 0)]
public class Tree : ScriptableObject
{
    public Sprite model;
    public List<TextVoice> messages;
    public UnityAction OnTalk;
    TextVoice message;

    [Obsolete]
    public void Preparing()
    {
        OnTalk += RandomTlak;
    }

    [Obsolete]
    public void RandomTlak()
    {
        
        Debug.Log($"tree {FindObjectsOfType<DelayDestroy>().Length}");
        foreach (var tree in FindObjectsOfType<DelayDestroy>())
        {
            Debug.Log(tree);
            Destroy(tree.gameObject);
        }

        int index = UnityEngine.Random.Range(0, messages.Count - 1);
        message = messages[index];
        message.Play(GameObject.FindWithTag("TreeFrame").transform);
    }
}

[Serializable]
public struct TextVoice
{
    public GameObject textChat;
    public string text;
    public AudioClip voice;

    public void Play(Transform parent)
    {
        Vector3 spawnLocation = Description.instance.GetRandomPositionInArea();
        
        GameObject obj = MonoBehaviour.Instantiate(textChat, parent);
        obj.GetComponent<RectTransform>().localPosition = spawnLocation;
        obj.transform.localScale = new Vector3((spawnLocation.x >= 0)?1: -1,1,1);

        GameObject child= obj.transform.GetChild(0).gameObject;
        child.GetComponent<TMP_Text>().text = text;
        child.transform.localScale = new Vector3((spawnLocation.x >= 0)?1: -1,1,1);
        
        AudioSource source = obj.GetComponent<AudioSource>();
        if (voice != null)
        {
            source.clip = voice;
            source.Play();
        }
        obj.AddComponent<DelayDestroy>();
    }
}

public class DelayDestroy : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
