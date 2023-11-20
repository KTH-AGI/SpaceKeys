// Courtesy of AGI Group 8 - Cubes Traveller.
// Available at: https://github.com/agi23-g8/cubes_traveller
// Potentially modified to suit the needs for SpaceKeys. 

using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class HostUI : MonoBehaviour
{
    [SerializeField]
    GameObject startHostDialog;

    [SerializeField]
    GameObject waitingForClientDialog;

    [SerializeField]
    TMP_InputField serverNameInput;

    [SerializeField]
    ServerNetworkDiscovery networkDiscovery;

    [SerializeField]
    NetworkManager networkManager;




    void Update()
    {
        if (networkManager.IsServer && networkManager.ConnectedClientsList.Count > 0)
        {
            startHostDialog.SetActive(false);
            waitingForClientDialog.SetActive(false);
            return;
        }
        
        startHostDialog.SetActive(!networkManager.IsServer);
        waitingForClientDialog.SetActive(networkManager.IsServer && networkManager.ConnectedClients.Count == 0);
        
        
    }

    public void StartDiscovery()
    {
        // start the network discovery
        networkDiscovery.ServerName = serverNameInput.text;
        networkManager.StartServer();
    }

    public void CancelDiscovery()
    {
        networkDiscovery.StopDiscovery();
        networkManager.Shutdown();
    }
}
