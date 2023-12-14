using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField_Animation : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            float torqueValue = Random.Range(2.0f, 40.0f);
            Vector3 randomDirection = Random.onUnitSphere;

            // Ensure the random direction is not the same as transform.up
            if (Vector3.Dot(randomDirection, transform.up) > 0.9f) // Adjust the threshold as needed
            {
                randomDirection = Vector3.Cross(randomDirection, transform.right).normalized;
            }
            transform.GetChild(i).GetComponent<Rigidbody>().AddTorque(randomDirection * torqueValue);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
