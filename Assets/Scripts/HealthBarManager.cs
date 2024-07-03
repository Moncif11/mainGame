using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarManager : MonoBehaviour {
    public void Start() {
        GameObject player = null;
        GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
        for (int i = 0; i < players.Length; i++) {
            if (players[i].gameObject.GetComponent<CapePlayerController>().IsOwner || !players[i].gameObject.GetComponent<CapePlayerController>().multiplayer){
                player = players[i];
            }
        }

        player.GetComponent<Health>().setHealthBar(GetComponent<Image>());
        player.GetComponent<CapePlayerController>().Start();
    }
}
