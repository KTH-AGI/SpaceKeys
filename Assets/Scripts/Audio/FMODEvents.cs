using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/**
 * Code inspired by https://www.youtube.com/watch?v=rcBHIOjZDpk
*/

public class FMODEvents : MonoBehaviour
{
    [field: Header("Melody Bb2")]
    [field: SerializeField] public EventReference melodyBb2 { get; private set; }
    
    [field: Header("Melody Db3")]
    [field: SerializeField] public EventReference melodyDb3 { get; private set; }

    [field: Header("Melody Eb3")]
    [field: SerializeField] public EventReference melodyEb3 { get; private set; }
    
    [field: Header("Melody F3")]
    [field: SerializeField] public EventReference melodyF3 { get; private set; }

    [field: Header("Melody Ab3")]
    [field: SerializeField] public EventReference melodyAb3 { get; private set; }
    
    [field: Header("Melody Bb3")]
    [field: SerializeField] public EventReference melodyBb3 { get; private set; }


    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }
}
