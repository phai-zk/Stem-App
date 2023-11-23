using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login_Animation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public void OnEditInputField(string txt)
    {
        if (txt != "")
            anim.Play("OnSelect");
        else
            anim.Play("OnDeSelect");

    }

}
