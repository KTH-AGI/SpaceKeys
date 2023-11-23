using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    public KeyCode key;
    bool active = false;

    GameObject note;
    [SerializeField] GameObject player;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float playerPositionX = player.transform.position.x;
        if (note != null)
        {
            float notePositionX = note.transform.position.x;
            float difference = Mathf.Abs(playerPositionX - notePositionX);
            Debug.Log(difference);

            if (Input.GetKeyDown(key) && active && difference < 1)
            {
                Destroy(note);
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
