using System;
using UnityEngine;

public class AccelerometerController : MonoBehaviour
{

    // Minimum threshold needed to surpass for movement detection. Todo: Confirm if input acceleration range is in [0,1]
    [SerializeField, Range(0,1)] 
    private float minimumAcceleration = 0.03f;

    private void Start()
    {
        Input.gyro.enabled = true;
    }
    
    public Vector3 GetUserAcceleration()
    {
        Vector3 acceleration = GetAccelerationAndCheckForDrift();

        acceleration = AndroidToUnity(acceleration);
        
        return acceleration;
    }

    private Vector3 AndroidToUnity(Vector3 androidAcceleration)
    {
        Vector3 unityAcceleration = androidAcceleration;
        
        // Remap acceleration axis to game axis
        unityAcceleration.x = -androidAcceleration.x;
        unityAcceleration.y = -androidAcceleration.y;

        if (unityAcceleration.sqrMagnitude > 1)
        {
            unityAcceleration.Normalize();
        }

        return unityAcceleration;
    }
    
    private Vector3 GetAccelerationAndCheckForDrift()
    {
        Vector3 acceleration = Vector3.zero;
        Vector3 userAcceleration = Input.gyro.userAcceleration;
        
        if (Math.Abs(userAcceleration.x) > minimumAcceleration )
        {
            acceleration.x = userAcceleration.x;
        }

        if (Math.Abs(userAcceleration.y) > minimumAcceleration )
        {
            acceleration.y = userAcceleration.y;
        }
        
        if (Math.Abs(userAcceleration.z) > minimumAcceleration )
        {
            acceleration.z = userAcceleration.z;
        }

        return acceleration;
    }
    
}
