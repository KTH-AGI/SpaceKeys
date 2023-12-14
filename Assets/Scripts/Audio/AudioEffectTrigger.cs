using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioEffectTrigger : MonoBehaviour
{
    // Enum to represent different audio effect options
    public enum AudioEffect
    {
        Reverb,
        LPFilter,
        HPFilter,
        Flanger
    }

    // Serialized field to choose the audio effect
    [SerializeField]
    private AudioEffect effect;

    /*[SerializeField]
    private float startValue = 1f;

    [SerializeField]
    private float endValue = 0f;*/
    private float startValue;
    private float endValue;
    
    // [SerializeField, Range(0f, 64f)]
    private float durationInSeconds = 8f;

    private string parameterName;
    //private float minValue;
    //private float maxValue;

    private void Awake()
    {
        switch (effect)
        {
            case AudioEffect.Reverb:
                parameterName = "Reverb Wetness";
                startValue = 1f;
                endValue = 0f;
                //minValue = 0f;
                //maxValue = 1f;
                break;
            case AudioEffect.LPFilter:
                parameterName = "Low-Pass Threshold";
                startValue = 2000f;
                endValue = 22000f;
                //minValue = 10f;
                //maxValue = 22000f;
                break;
            case AudioEffect.HPFilter:
                parameterName = "High-Pass Threshold";
                startValue = 4000f;
                endValue = 10f;
                //minValue = 10f;
                //maxValue = 22000f;
                break;
            case AudioEffect.Flanger:
                parameterName = "Flanger Mix";
                startValue = 0.5f;
                endValue = 0f;
                //minValue = 0f;
                //maxValue = 1f;
                break;
            default:
                break;
        } 
    }
    /*public float StartValue
    {
        get { return startValue; }
        set { startValue = value; }
    }
    public float EndValue
    {
        get { return endValue; }
        set { endValue = value; }
    }*/
    /*
    private void OnValidate()
    {
        clampValues();
    }
    private void clampValues()
    {
        //startValue = Mathf.Clamp(startValue, minValue, maxValue);
        //endValue = Mathf.Clamp(endValue, minValue, maxValue);
        durationInSeconds = Mathf.Clamp(durationInSeconds, 0f, 7200f);
    }*/

    private void OnTriggerEnter(Collider other)
    {
        if (AudioManager.instance != null)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                AudioManager.instance.SetParameterForSeconds(parameterName, startValue, endValue, durationInSeconds);
            }
        }
        else
        {
            Debug.LogError("AudioManager.instance is null. Make sure it's properly initialized.");
        }
    }
}
