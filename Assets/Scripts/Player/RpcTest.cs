using Unity.Netcode;
using UnityEngine;

public class RpcTest : NetworkBehaviour
{
    [HideInInspector] // Otherwise, we get a Bug: it throws an InvalidCastException. Should be fixed when updating NGO to 1.7.0
    public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Move();
        }
    }

    public void Move()
    {
        if (NetworkManager.Singleton.IsServer)
        {
            /*var randomPosition = GetRandomPositionOnPlane();
            transform.position = randomPosition;
            Position.Value = randomPosition;*/
        }
        else
        {
            SubmitPositionRequestServerRpc();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
    {
        Position.Value = GetRandomPositionOnPlane();
        Debug.Log("Client-Side: Moved");
    }

    static Vector3 GetRandomPositionOnPlane()
    {
        return new Vector3(Random.Range(-3f, 3f), 1f, 0f);
    }

    void Update()
    {
        Move();
        transform.position = Position.Value;
    }
}