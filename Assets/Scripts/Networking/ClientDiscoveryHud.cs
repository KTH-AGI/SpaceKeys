// Courtesy of AGI Group 8 - Cubes Traveller.
// Available at: https://github.com/agi23-g8/cubes_traveller
// Potentially modified to suit the needs for SpaceKeys. 

using System.Collections.Generic;
using System.Net;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using System;







#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
#endif

[RequireComponent(typeof(NetworkManager))]
public class ClientDiscoveryHud : NetworkDiscovery<DiscoveryBroadcastData, DiscoveryResponseData>
{

    [Serializable]
    public class ServerFoundEvent : UnityEvent<IPEndPoint, DiscoveryResponseData>
    {
    };

    NetworkManager m_NetworkManager;

    Dictionary<IPAddress, DiscoveryResponseData> discoveredServers = new Dictionary<IPAddress, DiscoveryResponseData>();

    [SerializeField]
    GameObject hostListPanel;

    [SerializeField]
    GameObject hostButtonPrefab;

    public ServerFoundEvent OnServerFoundEvent;

    void OnEnable()
    {
        m_NetworkManager = GetComponent<NetworkManager>();
        OnServerFoundEvent.AddListener(OnServerFound);
    }

    void Update()
    {
        if (m_NetworkManager.IsConnectedClient)
        {
            StopDiscovery();
            ClearHostList();
            return;
        }

        // if discovery is not running, start it
        if (!IsRunning)
        {
            StartClient();
            ClientBroadcast(new DiscoveryBroadcastData());
        }

    }

    public void RefreshHostList()
    {
        ClearHostList();

        StartClient();
        ClientBroadcast(new DiscoveryBroadcastData());
    }

    public void ClearHostList()
    {
        foreach (Transform child in hostListPanel.transform)
        {
            Destroy(child.gameObject);
        }
        discoveredServers.Clear();
    }

    void OnServerFound(IPEndPoint sender, DiscoveryResponseData response)
    {
        Debug.Log($"Found server at {sender.Address}:{response.Port} with name {response.ServerName}");
        discoveredServers[sender.Address] = response;
        // add a child button to the host list panel for each discovered server
        GameObject hostButton = Instantiate(hostButtonPrefab);
        hostButton.transform.SetParent(hostListPanel.transform);
        hostButton.GetComponentInChildren<TextMeshProUGUI>().text = $"{response.ServerName}[{sender.Address}]";
        hostButton.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() =>
        {
            Debug.Log($"Connecting to {response.ServerName}[{sender.Address}:{response.Port}]");

            UnityTransport transport = (UnityTransport)m_NetworkManager.NetworkConfig.NetworkTransport;
            transport.SetConnectionData(sender.Address.ToString(), response.Port);
            m_NetworkManager.StartClient();
        });
    }

    protected override bool ProcessBroadcast(IPEndPoint sender, DiscoveryBroadcastData broadCast, out DiscoveryResponseData response)
    {
        // This is not a server, so we don't need to send a response
        response = new DiscoveryResponseData();
        return false;
    }

    protected override void ResponseReceived(IPEndPoint sender, DiscoveryResponseData response)
    {
        OnServerFoundEvent.Invoke(sender, response);
    }
}
