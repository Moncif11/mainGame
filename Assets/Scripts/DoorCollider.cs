using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCollider : MonoBehaviour {
    public GameObject entranceManager;
    
    private void OnTriggerEnter2D(Collider2D col) {
        GetComponent<SpriteRenderer>().color = new Color32(107, 253, 129, 255);
        if (name == "Door1") {
            entranceManager.GetComponent<EntranceManager>().door1IsReady();
        }
        else {
            entranceManager.GetComponent<EntranceManager>().door2IsReady();
        }
    }
    private void OnTriggerExit2D(Collider2D col) {
        GetComponent<SpriteRenderer>().color = new Color32(253, 137, 22, 255);
        try { if (name == "Door1") {
                entranceManager.GetComponent<EntranceManager>().door1IsUnready();
            }
            else {
                try {
                    entranceManager.GetComponent<EntranceManager>().door2IsUnready();
                }
                catch (Exception e) {
                    print("nope");
                }
            }}
        catch (Exception e) {
            
        }
    }
}
