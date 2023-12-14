using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private Vector3 rotationSpeed;

    private Vector3 rotation;
    void Start()
    {
        rotation = new Vector3(0, 0, 0);
    }

    private void FixedUpdate()
    {
        rotation += Time.deltaTime * rotationSpeed;

        if (rotation.x > 360.0f)
        {
            rotation.x = 0.0f;
        }

        if (rotation.y > 360.0f)
        {
            rotation.y = 0.0f;
        }

        if (rotation.z > 360.0f)
        {
            rotation.z = 0.0f;
        }
        
        transform.localRotation = Quaternion.Euler(rotation);
    }
}
