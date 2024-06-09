using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkManagerUI : MonoBehaviour {
    [SerializeField] private Button serverButton;
    [SerializeField] private Button hostButton;
    [SerializeField] private Button clientButton;
    [SerializeField] private Button leaveButton;
    [SerializeField] private TMP_InputField IPAdressInput;
    [SerializeField] private NetworkManager networkManager;
    private String ipAdress = "";

    private void Awake() {
        serverButton.onClick.AddListener(() => {
            NetworkManager.Singleton.StartServer();
        });
        hostButton.onClick.AddListener(() => {
            ipAdress = IPAdressInput.text;
            if (ipAdress == "") {
                ipAdress = "127.0.0.1";
            }
            Debug.Log(ipAdress);
            networkManager.GetComponent<UnityTransport>().ConnectionData.Address=ipAdress;
            NetworkManager.Singleton.StartHost();
        });
        clientButton.onClick.AddListener(() => {
            ipAdress = IPAdressInput.text;
            if (ipAdress == "") {
                ipAdress = "127.0.0.1";
            }
            Debug.Log(ipAdress);
            networkManager.GetComponent<UnityTransport>().ConnectionData.Address=ipAdress;
            NetworkManager.Singleton.StartClient();
        });
        leaveButton.onClick.AddListener(() => {
            NetworkManager.Singleton.Shutdown();
        });
    }
}
