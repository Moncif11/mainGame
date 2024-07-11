using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UltBarManager : MonoBehaviour {
    public void Start() {
        GameObject player = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
        for (int i = 0; i < players.Length; i++) {
            players[i].GetComponent<CapePlayerController>().Start();
            if (players[i].gameObject.GetComponent<CapePlayerController>().IsOwner || !players[i].gameObject.GetComponent<CapePlayerController>().multiplayer){
                player = players[i];
            }
        }
        player.GetComponent<AbilityManager>().setUltBar(GetComponent<Image>());
    }
}