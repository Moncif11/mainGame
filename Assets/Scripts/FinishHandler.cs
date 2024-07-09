using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishHandler : MonoBehaviour
{
    public NetworkManager networkManager;
    private void OnTriggerEnter2D(Collider2D col) {
        networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
        Debug.Log(SceneManager.GetActiveScene().name);
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "SingleLevel1") {
            Debug.Log("Load next level");
            SceneManager.LoadScene("SingleLevel2");
        }
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "SingleLevel2") {
            Debug.Log("Load next level");
            SceneManager.LoadScene("SingleLevel3");
        }
        if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "SingleLevel3") {
            Debug.Log("Load finish screen");
            //SceneManager.LoadScene("FinishScreen"); //TO-DO
        }
        if (networkManager != null) {
            Debug.Log("this scene: "+SceneManager.GetActiveScene().name);
            Debug.Log("col.object.tag : "+col.gameObject.tag);
            if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "MultiLevel1") {
                Debug.Log("Load next level");
                networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
                networkManager.SceneManager.LoadScene("MultiLevel2", LoadSceneMode.Single);   
            }
            if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "MultiLevel2") {
                Debug.Log("Load next level");
                networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
                networkManager.SceneManager.LoadScene("MultiLevel3", LoadSceneMode.Single);   
            } 
            if (col.gameObject.tag == "Player" && SceneManager.GetActiveScene().name == "MultiLevel3") {
                Debug.Log("Load next level");
                networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
                //networkManager.SceneManager.LoadScene("MultiFinishScreen", LoadSceneMode.Single); //TO-DO
            } 
        }
    }

}
