using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

/**
 * Parts of code from https://github.com/shapedbyrainstudios/fmod-audio-system
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
        gameObject.SetActive(false);  // TODO: do something else for drums, bass and effects?
        Debug.Log("Music object deactivated");

        string musicObject = gameObject.tag;

        switch(musicObject) 
        {
        /*case "Drums 1":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.drums1, this.transform.position);  // TODO: don't use PlayOneShot
            Debug.Log("Drums 1 audio played");
            break;
        case "Drums 2":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.drums2, this.transform.position);  // TODO: don't use PlayOneShot
            Debug.Log("Drums 2 audio played");
            break;
        case "Bass 1":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.bass1, this.transform.position);  // TODO: don't use PlayOneShot
            Debug.Log("Bass 1 audio played");
            break;*/
        case "Melody Bb2":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyBb2, this.transform.position);  // or Camera.main.transform.position
            Debug.Log("Melody Bb2 audio played");
            break;
        case "Melody Db3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyDb3, this.transform.position);
            Debug.Log("Melody Db3 audio played");
            break;
        case "Melody Eb3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyEb3, this.transform.position);
            Debug.Log("Melody Eb3 audio played");
            break;
        case "Melody F3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyF3, this.transform.position);
            Debug.Log("Melody F3 audio played");
            break;
        case "Melody Ab3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyAb3, this.transform.position);
            Debug.Log("Melody Ab3 audio played");
            break;
        case "Melody Bb3":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.melodyBb3, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Melody Bb3 audio played");
            break;
        case "Space Probe":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.spaceProbe, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Space probe audio played");
            break;
        /*case "Harmony 1":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.harmony1, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Harmony 1 audio played");
            break;
        case "Harmony 2":
            AudioManager.instance.PlayOneShot(FMODEvents.instance.harmony2, this.transform.position);  // change to this later?: Camera.main.transform.position
            Debug.Log("Harmony 2 audio played");
            break;*/
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
