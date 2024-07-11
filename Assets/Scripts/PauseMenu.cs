using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;
    private bool singlePlayer = false;

    void Start() {
        if (SceneManager.GetActiveScene().name == "SingleLevel1" ||
            SceneManager.GetActiveScene().name == "SingleLevel2" ||
            SceneManager.GetActiveScene().name == "SingleLevel3") {
            singlePlayer = true;
        }
        else {
            singlePlayer = false;
        }
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameIsPaused) {
                resume();
            }
            else {
                pause();
            }
        }
    }

    public void resume() {
        PauseMenuUI.SetActive(false);
        if (singlePlayer) {
            Time.timeScale = 1f;
            GameIsPaused = false;   
        }
    }

    void pause() {
        PauseMenuUI.SetActive(true);
        if (singlePlayer) {
            Time.timeScale = 0f;
            GameIsPaused = true;   
        }
    }

    public void restart() {
        if (singlePlayer) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            resume();   
        }
        else {
            NetworkManager networkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
            networkManager.SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single); 
        }
    }
    
    public void menu() {
        if (singlePlayer) {
            SceneManager.LoadScene("Main Menu");
            resume();
        }
        else {
            NetworkManager.Singleton.Shutdown();
            GameObject networkManager = GameObject.Find("NetworkManager");
            Destroy(networkManager);
            SceneManager.LoadScene("Main Menu");
        }
    }
}
