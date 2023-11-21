using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerDataRPC : NetworkBehaviour
{
    [HideInInspector] // Otherwise, we get a Bug: it throws an InvalidCastException. Should be fixed when updating NGO to 1.7.0
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    [HideInInspector] 
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();
    [HideInInspector] 
    public NetworkVariable<Vector3> Acceleration = new NetworkVariable<Vector3>();

    private Rigidbody rb;
    
    // For development purposes. Remove when pushing for production.
    private bool debug = true;
    private TextMeshProUGUI DebugText;

    private String debugText = "";
    ////////////////////////////////////////////////////////////////
    
    
    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton.IsClient)
        { 
            Input.gyro.enabled = true;
        }
    }

    private void Start()
    {
        if (debug)
        {
            DebugText = FindObjectOfType<DebugHelper>().DebugText;
        }

        rb = GetComponent<Rigidbody>();
    }

    [ServerRpc(RequireOwnership = false)]
    void SubmitPositionRequestServerRpc(Vector3 acceleration, ServerRpcParams rpcParams = default)
    {
        
        // Get Gyro Rotation
        // Rotation.Value = gyroRotation;
        // Add Acceleration Code Here
        // Position.Value = GetRandomPositionOnPlane();
        Acceleration.Value = acceleration;
        

    }

    Quaternion GetGyroValues()
    {
        // TODO might have to fix x axis alignment
        return Input.gyro.attitude;
    }
    
    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, 0f);
    }

    void UpdateMovement()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            debugText += "|| Gyro: " + Input.gyro.userAcceleration;
            SubmitPositionRequestServerRpc(Input.gyro.userAcceleration);
        }
        
        UpdateTransform();
    }
    
    void UpdateTransform()
    {
     
        // transform.rotation = Rotation.Value;
        
        // Todo add acceleration to modify position
        // transform.position = Position.Value;

        transform.position += Acceleration.Value;

    }

    void FixedUpdate()
    {
        debugText = "";
        if (debug)
        {
            // debugText += " || IsServer: " + IsServer + " || IsClient: " + IsClient;
            debugText += " || NetworkManager Client: " + NetworkManager.Singleton.IsClient + " || Network Server: " + NetworkManager.Singleton.IsServer;
            if (NetworkManager.Singleton.IsServer)
            {
                debugText += " || Connected: " + NetworkManager.Singleton.ConnectedClients.Count;
            }
        }

        UpdateMovement();

        if (debug)
        {
            debugText += " || GyroAcceleration: " + Acceleration.Value;
            DebugText.text = debugText;
        }
    }
}
