using System;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using Random = UnityEngine.Random;

public class RpcTestNew : NetworkBehaviour
{
    [SerializeField] private TMP_Text DebugText;
    
    [HideInInspector] // Otherwise, we get a Bug: it throws an InvalidCastException. Should be fixed when updating NGO to 1.7.0
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
    [HideInInspector] 
    public NetworkVariable<Quaternion> Rotation = new NetworkVariable<Quaternion>();

    // private NetworkObject _networkObject;
    
    public override void OnNetworkSpawn()
    {
        if (NetworkManager.Singleton.IsClient)
        { 
            // Input.gyro.enabled = true;
        }
    }

    /*public void Start()
    {
        if (NetworkManager.Singleton.IsClient)
        {
            // Input.gyro.enabled = true;
        }

        if (NetworkManager.Singleton.IsServer)
        {
            _networkObject = GetComponent<NetworkObject>();
            _networkObject.Spawn();
        }
    }*/

    [ServerRpc(RequireOwnership = false)]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        
        // DebugText.text = "Inside if! " + Input.gyro.attitude;
        
        // Get Gyro Rotation
        // Rotation.Value = Input.gyro.attitude;
        // Debug.Log("Rotating: " + Rotation.Value);
        
        // Add Acceleration Code Here
        Position.Value = GetRandomPositionOnPlane();
        DebugText.text += "   Updated Position ";


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
        DebugText.text += ("  |||  Update Transform  ||| ");
        if (NetworkManager.Singleton.IsClient)
        {
            DebugText.text += " ||| Inside Submit Position  |||";
            SubmitPositionRequestServerRpc();
        }
    }
    
    void UpdateTransform()
    {
     
        // transform.rotation = Rotation.Value;
        
        // Todo add acceleration to modify position
        transform.position = Position.Value;
        
        DebugText.text += ("  |||  Updated ||| ");
    }

    void Update()
    {
        DebugText.text = "";
        UpdateMovement();
        UpdateTransform();

        String tmp = "    IsClient: " + IsClient + " NetworkManager Client: " + NetworkManager.Singleton.IsClient + "  NServer: " + NetworkManager.Singleton.IsServer;
        DebugText.text += tmp;
    }
}
