using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode key;
    bool active = false;

    GameObject note;
    [SerializeField] GameObject player;
    
    private AudioSource audioSource;

    [SerializeField] 
    private int radarDiameter;

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
                audioSource = note.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    // Debug.Log("Audio source volume: " + audioSource.volume);
                    // Debug.Log("Distance cam-music object: " + Vector3.Distance(note.transform.position, Camera.main.transform.position));
                    // audioSource.PlayOneShot(audioSource.clip);
                    if (!audioSource.isPlaying)
                    {
                        audioSource.Play();
                        Debug.Log("Music object audio played with Play()");
                    }
                    
                    Debug.Log("Music object audio played");
                }
                note.SetActive(false);
                note = null;
            }
        }
        
        
    }

    void OnTriggerEnter(Collider col)
    {
        active = true;
        if (col.gameObject.CompareTag("Note"))
        {
            note = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        active = false;
    }
}
