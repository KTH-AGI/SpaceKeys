/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Activator : MonoBehaviour
{
    public KeyCode key;
    bool active = false;

    GameObject note;
    [SerializeField] GameObject player;
    
    AudioSource audioSource;

    [SerializeField] private int radarDiameter;

    [SerializeField] private EventReference musicObjectSound;

    // Start is called before the first frame update
    void Start()
    {
        radarDiameter = 4;
    }

    // Update is called once per frame
    void Update()
    {
        float playerPositionX = player.transform.position.x;
        if (note != null)
        {
            float notePositionX = note.transform.position.x;
            float difference = Mathf.Abs(playerPositionX - notePositionX);
            // Debug.Log(difference);

            if (active && difference < radarDiameter)
            {
                CollectMusicObject();
            }
        }
        
        
    }

    void OnTriggerEnter(Collider col)
    {
        active = true;
        if (col.gameObject.CompareTag("Note"))
        {
            note = col.gameObject;
            audioSource = note.GetComponent<AudioSource>();
        }
    }

    void OnTriggerExit(Collider col)
    {
        active = false;
    }

    void CollectMusicObject()
    {
        // AudioSource.PlayClipAtPoint(audioSource.clip, Camera.main.transform.position);          
        AudioManager.instance.PlayOneShot(musicObjectSound, Camera.main.transform.position);  // change to this later?: this.transform.position
        Debug.Log("Music object audio played");
        note.SetActive(false);
        note = null;
    }
}

*/