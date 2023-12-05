using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/**
 * Code inspired by https://www.youtube.com/watch?v=rcBHIOjZDpk
*/

[RequireComponent(typeof(Collider))]

public class MusicObject : MonoBehaviour
{
    void Start()
    {
        Assert.IsTrue(gameObject.GetComponent<Collider>().isTrigger);
    }

    private void OnTriggerEnter(Collider other)  // should we use OnTriggerEnter2D?
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CollectMusicObject();
        }        
    }

    private void CollectMusicObject()
    {
        gameObject.SetActive(false);
        Debug.Log("Music object deactivated");

        string musicObject = gameObject.tag;

        switch(musicObject) 
        {
        case "Melody Bb2":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyBb2, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Bb2 audio played");
            break;
        case "Melody Db3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyDb3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Db3 audio played");
            break;
        case "Melody Eb3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyEb3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Eb3 audio played");
            break;
        case "Melody F3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyF3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody F3 audio played");
            break;
        case "Melody Ab3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyAb3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Ab3 audio played");
            break;
        case "Melody Bb3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyBb3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Bb3 audio played");
            break;
        default:
            Debug.Log("Music object without audio tag encountered: " + musicObject);
            break;
        }

        /*
        if (gameObject.CompareTag("Melody Bb2"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyBb2, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Bb2 audio played");
        }
        if (gameObject.CompareTag("Melody Db3"))
        {
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyDb3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Db3 audio played");
        }
        */
        
        // todo do score logic
    }
}