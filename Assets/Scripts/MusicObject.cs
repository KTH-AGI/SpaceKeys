using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using FMODUnity;

/**
 * Parts of code from https://github.com/shapedbyrainstudios/fmod-audio-system
*/
[RequireComponent(typeof(Collider))]
public class MusicObject : MonoBehaviour
{
    EventReference sound;
    string musicObject;
    bool validMusicObject;
    private float x;
    private float y;

    private float r;

    // public float UFOHeightRatio;
    private string parameterName = "Brightness";

    private float _UFOHeightRatio;

    // Event for when the player collides with a note
    public static event Action<Vector3, Vector3, float> OnCollisionNote;
    bool actionEvent = true;

    // Public property with a public getter and private setter
    public float UFOHeightRatio
    {
        get
        {
            // Getter logic goes here
            return _UFOHeightRatio;
        }
        private set
        {
            // Setter logic goes here (private means it can only be set within this class)
            _UFOHeightRatio = value;
        }
    }

    void Start()
    {
        Assert.IsTrue(gameObject.GetComponent<Collider>().isTrigger);
        r = CircularGrid.radius;
    }

    private void OnTriggerEnter(Collider other) // should we use OnTriggerEnter2D?
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Time that has passed until collision: " + Time.time);
            CollectMusicObject();
            OnCollisionNote?.Invoke(this.transform.position, other.transform.position,
                other.GetComponent<SphereCollider>().radius * other.transform.lossyScale.x +
                this.GetComponent<SphereCollider>().radius * this.transform.lossyScale.x);
        }
    }

    private void CollectMusicObject()
    {
        gameObject.SetActive(false);
        Debug.Log("Music object deactivated");

        y = this.transform.position.y;
        x = this.transform.position.x;

        UFOHeightRatio = (y / (float)(2 * Math.Sqrt(Math.Pow(r, 2) - Math.Pow(x, 2))) + 0.5f);

        musicObject = gameObject.tag;
        validMusicObject = true;
        actionEvent = true;

        switch (musicObject)
        {
            case "Melody Ab2":
                sound = FMODEvents.instance.melodyAb2;
                break;
            case "Melody Bb2":
                sound = FMODEvents.instance.melodyBb2;
                break;
            case "Melody C3":
                sound = FMODEvents.instance.melodyC3;
                break;
            case "Melody Db3":
                sound = FMODEvents.instance.melodyDb3;
                break;
            case "Melody Eb3":
                sound = FMODEvents.instance.melodyEb3;
                break;
            case "Melody F3":
                sound = FMODEvents.instance.melodyF3;
                break;
            case "Melody Gb3":
                sound = FMODEvents.instance.melodyGb3;
                break;
            case "Melody Ab3":
                sound = FMODEvents.instance.melodyAb3;
                break;
            case "Melody Bb3":
                sound = FMODEvents.instance.melodyBb3;
                break;
            case "Space Probe":
                sound = FMODEvents.instance.spaceProbe;
                break;
            case "Star Cluster 1":
                AudioManager.instance.InitializeEventForDuration(FMODEvents.instance.harmony3, 16);
                actionEvent = false;
                break;
            case "Star Cluster 2":
                AudioManager.instance.InitializeEventForDuration(FMODEvents.instance.harmony4, 16);
                actionEvent = false;
                break;
            default:
                validMusicObject = false;
                Debug.Log("Game object without audio tag encountered: " + musicObject);
                break;
        }

        if (validMusicObject && actionEvent)
        {
            Debug.Log(parameterName + " for " + musicObject + ": " + UFOHeightRatio);
            AudioManager.instance.SetParameterAndPlay(sound, parameterName,
                UFOHeightRatio); // this.transform.position or Camera.main.transform.position
            Debug.Log(musicObject + " audio played");
        }

        // todo do score logic
    }
}