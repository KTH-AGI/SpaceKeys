using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GyroController : MonoBehaviour
{

    [SerializeField] Transform gyro;

    private Quaternion initialRotation;
    private Quaternion initialGyroRotation;
    
    IEnumerator Start()
    {
        yield return new WaitForSeconds(1);
     
        RecalibrateGyro();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void RecalibrateGyro()
    {
        initialGyroRotation = GyroToUnity(gyro.rotation);
        initialRotation = Quaternion.identity;
    }
    private static Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.z, q.y, -q.w);
    }
}
