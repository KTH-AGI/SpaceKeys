using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightFollow : MonoBehaviour
{

    public Transform target; // The GameObject to follow

    void Update()
    {
        // Only follow in the x-axis
        Vector3 newPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

        // Set the position of the empty GameObject
        transform.position = newPosition;
    }
}
