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

    private AccelerometerController _accelerometerController;

    [SerializeField]
    private float smoothingFactor = 0.5f;
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
            _accelerometerController = FindObjectOfType<AccelerometerController>();
        }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (debug)
        {
            DebugText = FindObjectOfType<DebugHelper>().DebugText;
        }
    }

    
    /// <summary>
    /// Method used to update information received from client. Executed Server-side.
    /// </summary>
    /// <param name="acceleration"></param> acceleration value obtained from client.
    /// <param name="rpcParams"></param>
    [ServerRpc(RequireOwnership = false)]
    void SubmitPositionRequestServerRpc(Vector3 acceleration, ServerRpcParams rpcParams = default)
    {
        
        // Get Gyro Rotation
        // Rotation.Value = gyroRotation;
        Acceleration.Value = acceleration;
    }

    Quaternion GetGyroValues()
    {
        // TODO might have to fix x axis alignment
        return Input.gyro.attitude;
    }

    void UpdateMovement()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            Vector3 acceleration = _accelerometerController.GetUserAcceleration();
            debugText += "|| Gyro: " + Input.gyro.userAcceleration;
            debugText += "|| Acceleration added: " + acceleration;
            
            // Send data to Server
            SubmitPositionRequestServerRpc(acceleration);
        }
        
        UpdateTransform();
    }
    
    void UpdateTransform()
    {
        // transform.rotation = Rotation.Value;
        // transform.position = Position.Value;

        // transform.position += Acceleration.Value;
        
        // Apply Exponential Smoothing
        Vector3 tmp = transform.position + Acceleration.Value;

        transform.position = smoothingFactor * transform.position + (1 - smoothingFactor) * tmp;


        // transform.Translate(Acceleration.Value);  // Does the same thing?
        // rb.AddForce(Acceleration.Value * 10f, ForceMode.Acceleration);

    }

    void FixedUpdate()
    {
        if (debug)
        {
            debugText = "";
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
