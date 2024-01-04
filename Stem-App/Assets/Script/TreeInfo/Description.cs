using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System;
using System.Collections;

public class Description : MonoBehaviour {
    public Image modelBox; 
    public TMP_Text treeName; 
    public TMP_Text temp; 
    public TMP_Text moisture; 
    [SerializeField]
    private RectTransform spawnAreaSize;
    
    public static Description instance;

    [Obsolete]
    public void PrepareUI(TreeInfo info)
    {
        instance = this;
        this.gameObject.SetActive(true);

        modelBox.sprite = info.treeModel.model;
        treeName.text = info.treeName;

        Match match;
        match = info.EnCodeData(info.tempData);
        
        // Debug.Log($"{info.tempData}");
        // Debug.Log($"{match.Groups.Count}");
        // Debug.Log($"{match.Groups[1]}");
        int newTemp = Int32.Parse(match.Groups[match.Groups.Count-1].Value) - UnityEngine.Random.Range(-2, 5);
        temp.text = $"{newTemp}°C";

        match = info.EnCodeData(info.moistureData);
        int newMoisture = Int32.Parse(match.Groups[2].Value) - UnityEngine.Random.Range(-2, 5);
        moisture.text = $"{newMoisture}%";

        Button button = modelBox.GetComponent<Button>();
        button.onClick.AddListener(()=> info.treeModel.RandomTlak());
        StartCoroutine(RandomTalk(info));
    }

    [Obsolete]
    IEnumerator RandomTalk(TreeInfo info)
    {
        info.treeModel.RandomTlak();
        yield return new WaitForSeconds(30);
        StartCoroutine(RandomTalk(info));
    }

    public Vector3 GetRandomPositionInArea()
    {
        float randomX = UnityEngine.Random.Range(-spawnAreaSize.sizeDelta.x / 3f, spawnAreaSize.sizeDelta.x / 3f);
        float randomY = UnityEngine.Random.Range(-spawnAreaSize.sizeDelta.y / 4f, spawnAreaSize.sizeDelta.y / 4f);
        return new Vector3(randomX, randomY , 0);
    }

    public void OnScrolling(float heigh)
    {

    }
}