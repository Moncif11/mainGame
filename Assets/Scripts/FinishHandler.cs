using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishHandler : MonoBehaviour
{
    public NetworkManager networkManager;
    private void OnTriggerEnter2D(Collider2D col) {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "SingleLevel1") {
            Debug.Log("Load next level");
            SceneManager.LoadScene("SingleLevel2");
        }
        if (networkManager != null) {
            if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "Multilevel1") {
                Debug.Log("Load next level");
                networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
                networkManager.SceneManager.LoadScene("MultiLevel2", LoadSceneMode.Single);   
            }   
        }
    }

}
