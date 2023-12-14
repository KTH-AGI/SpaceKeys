using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

/**
 * Parts of code from https://github.com/shapedbyrainstudios/fmod-audio-system
*/

public class FMODEvents : MonoBehaviour
{
    [field: Header("Background 1")]
    [field: SerializeField] public EventReference background1 { get; private set; }

    [field: Header("Background 2")]
    [field: SerializeField] public EventReference background2 { get; private set; }

    [field: Header("Background 3")]
    [field: SerializeField] public EventReference background3 { get; private set; }

    [field: Header("Drums 1")]
    [field: SerializeField] public EventReference drums1 { get; private set; }

    [field: Header("Drums 2")]
    [field: SerializeField] public EventReference drums2 { get; private set; }

    [field: Header("Bass 1")]
    [field: SerializeField] public EventReference bass1 { get; private set; }

    [field: Header("Harmony 1")]
    [field: SerializeField] public EventReference harmony1 { get; private set; }

    [field: Header("Harmony 2")]
    [field: SerializeField] public EventReference harmony2 { get; private set; }

    [field: Header("Harmony 3")]
    [field: SerializeField] public EventReference harmony3 { get; private set; }

    [field: Header("Harmony 4")]
    [field: SerializeField] public EventReference harmony4 { get; private set; }
    
    [field: Header("Melody Ab2")]
    [field: SerializeField] public EventReference melodyAb2 { get; private set; }
    
    [field: Header("Melody Bb2")]
    [field: SerializeField] public EventReference melodyBb2 { get; private set; }

    [field: Header("Melody C3")]
    [field: SerializeField] public EventReference melodyC3 { get; private set; }
    
    [field: Header("Melody Db3")]
    [field: SerializeField] public EventReference melodyDb3 { get; private set; }

    [field: Header("Melody Eb3")]
    [field: SerializeField] public EventReference melodyEb3 { get; private set; }
    
    [field: Header("Melody F3")]
    [field: SerializeField] public EventReference melodyF3 { get; private set; }

    [field: Header("Melody Gb3")]
    [field: SerializeField] public EventReference melodyGb3 { get; private set; }

    [field: Header("Melody Ab3")]
    [field: SerializeField] public EventReference melodyAb3 { get; private set; }
    
    [field: Header("Melody Bb3")]
    [field: SerializeField] public EventReference melodyBb3 { get; private set; }

    [field: Header("Space Probe")]
    [field: SerializeField] public EventReference spaceProbe { get; private set; }



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
