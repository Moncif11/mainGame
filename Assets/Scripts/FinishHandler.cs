using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishHandler : MonoBehaviour
{
    public NetworkManager networkManager;
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log("Load next level");
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        networkManager.SceneManager.LoadScene("MultiLevel2", LoadSceneMode.Single);
    }

}
