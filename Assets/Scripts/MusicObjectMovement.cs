using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicObjectMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current position
        Vector3 currentPosition = transform.position;

        float zMovement = movementSpeed * Time.deltaTime;
        currentPosition.z += zMovement;

        // Apply the new position
        transform.position = currentPosition;

    }
}
