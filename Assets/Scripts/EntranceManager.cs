using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceManager : MonoBehaviour {
    private bool door1Ready = false;
    private bool door2Ready = false;
    public NetworkManager networkManager; 
    
    public void door1IsReady() {
        Debug.Log("door1 is Ready");
        door1Ready = true;
        checkBothReady();
    }
    public void door2IsReady() {
        Debug.Log("door2 is Ready");
        door2Ready = true;
        checkBothReady();
    }
    public void door1IsUnready() {
        Debug.Log("door1 is unReady");
        door1Ready = false;
    }
    public void door2IsUnready() {
        Debug.Log("door2 is Unready");
        door2Ready = false;
    }

    private void checkBothReady() {
        if (door1Ready && door2Ready) {
            networkManager.SceneManager.LoadScene("MultiLevel1", LoadSceneMode.Single);
        }
    }
}
