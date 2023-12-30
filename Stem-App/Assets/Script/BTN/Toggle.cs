using UnityEngine;
using System;
using UnityEngine.Events;
using UnityEngine.UI;
using Unity.VisualScripting;
using NUnit.Framework.Internal;

[RequireComponent(typeof(Button))]
public class Toggle : MonoBehaviour
{
    [SerializeField]
    private GameObject active;

    [SerializeField]
    private GameObject unActive;

    private Button button;
    [HideInInspector]
    public bool isOn = false;

    [Serializable]
    public class EventAction : UnityEvent<bool> { }
    [SerializeField]
    private EventAction action;

    public void SetUp() {
        OnToggle();
        button = GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(()=>OnToggle());    
    }

    private void OnToggle()
    {
        isOn = (isOn == false)? true: false;
        Debug.Log("HI");
        SwitchToggle();
        action?.Invoke(isOn);
    }

    private void SwitchToggle()
    {
        active.SetActive(isOn);
        unActive.SetActive(!isOn);
    }

    public void Test1(bool test)
    {
        Debug.Log("Test1 : " + test + " : " + gameObject.name);
    }

    public void Test2(bool test)
    {
        Debug.Log("Test2 : " + test + " : " + gameObject.name);
    }

    public void Test3(bool test)
    {
        Debug.Log("Test3 : " + test + " : " + gameObject.name);
    }
}
