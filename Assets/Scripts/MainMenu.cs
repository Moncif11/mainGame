using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class MainMenu : MonoBehaviour
{
    public void Play(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
    }
    public void HowToPlay(){
        SceneManager.LoadScene("HowToPlay");
    }
    public void Menu(){
        SceneManager.LoadScene("MainMenu");

    }
    public void Quit(){ 
        Application.Quit(); 
        Debug.Log("You have Quit the Game. Bye Bye:)");
    }
}
