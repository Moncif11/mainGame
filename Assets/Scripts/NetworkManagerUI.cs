using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour {
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private TMP_InputField JoinCodeInput;
    [SerializeField] private NetworkManager networkManager;
    [SerializeField] UnityTransport transport;
    private String joinCode = "";

    private async void Start() {
        await UnityServices.InitializeAsync();

        if (!AuthenticationService.Instance.IsSignedIn) {
            AuthenticationService.Instance.SignedIn += () => {
                Debug.Log("Signed in "+ AuthenticationService.Instance.PlayerId);
            };
            await AuthenticationService.Instance.SignInAnonymouslyAsync();   
        }
    }
    
    private void Awake() {
        hostButton.onClick.AddListener(() => {
            //networkManager.GetComponent<UnityTransport>().ConnectionData.Address="127.0.0.1";
            //networkManager.GetComponent<UnityTransport>().ConnectionData.Address="192.168.153.1";
            //networkManager.GetComponent<UnityTransport>().ConnectionData.Port=8888;
            CreateRelay();
            //Debug.Log(GetIP(ADDRESSFAM.IPv4));
            //Debug.Log(GetLocalIPAddress());
        });
        clientButton.onClick.AddListener(() => {
            joinCode = JoinCodeInput.text;
            JoinRelay(joinCode);
        });
        leaveButton.onClick.AddListener(() => {
            NetworkManager.Singleton.Shutdown();
        });
    }
   
    private async void CreateRelay() {
        try {
            Allocation allocation = await RelayService.Instance.CreateAllocationAsync(3);

            string joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
            Debug.Log(joinCode);
            JoinCodeInput.text = joinCode;

            NetworkManager.Singleton.GetComponent<UnityTransport>().SetHostRelayData(
                allocation.RelayServer.IpV4,
                (ushort)allocation.RelayServer.Port,
                allocation.AllocationIdBytes,
                allocation.Key,
                allocation.ConnectionData
                );
            
            NetworkManager.Singleton.StartHost();
        }
        catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }
    private async void JoinRelay(string joinCode) {
        try {
            Debug.Log("Joining Relay with " + joinCode);
            JoinAllocation joinAllocation= await RelayService.Instance.JoinAllocationAsync(joinCode);
            
            NetworkManager.Singleton.GetComponent<UnityTransport>().SetClientRelayData(
                joinAllocation.RelayServer.IpV4,
                (ushort)joinAllocation.RelayServer.Port,
                joinAllocation.AllocationIdBytes,
                joinAllocation.Key,
                joinAllocation.ConnectionData,
                joinAllocation.HostConnectionData
            );
            
            NetworkManager.Singleton.StartClient();
        }
        catch (RelayServiceException e) {
            Debug.Log(e);
        }
    }
}
