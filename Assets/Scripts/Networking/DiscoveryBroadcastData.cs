// Courtesy of AGI Group 8 - Cubes Traveller.
// Available at: https://github.com/agi23-g8/cubes_traveller
// Potentially modified to suit the needs for SpaceKeys. 

using Unity.Netcode;
using UnityEngine;

public struct DiscoveryBroadcastData : INetworkSerializable
{
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
    }
}
