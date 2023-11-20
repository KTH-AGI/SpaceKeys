// Courtesy of AGI Group 8 - Cubes Traveller.
// Available at: https://github.com/agi23-g8/cubes_traveller
// Potentially modified to suit the needs for SpaceKeys. 

using Unity.Netcode;
using UnityEngine;

public struct DiscoveryResponseData: INetworkSerializable
{
    public ushort Port;

    public string ServerName;
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref Port);
        serializer.SerializeValue(ref ServerName);
    }
}
