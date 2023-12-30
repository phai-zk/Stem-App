using UnityEngine.Audio;
using UnityEngine;
using System;

[RequireComponent(typeof(Toggle))]
public class SoundSetting : MonoBehaviour
{
    [SerializeField]
    AudioMixer mixer;        
    public float max = -10;
    public float min = -80;

    public void OnValueChange(bool isOn)
    {
        if (isOn)
            On();
        else
            Off();
    }

   private void On()
   {
        mixer.SetFloat("volume",max);
   }

   private void Off()
   {
        mixer.SetFloat("volume",min);
   }
}
