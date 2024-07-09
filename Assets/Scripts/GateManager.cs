using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateManager : MonoBehaviour {
    public GameObject lowerPart;
    public GameObject upperPart;
    private float moveSpeed = 0.1f;
    private bool makeOpen;
    private float moveDistance = 2.5f;
    private float lowerPartStartY;
    
    void Start() {
        lowerPartStartY = lowerPart.transform.position.y;
        makeOpen = false;
    }

    public void openGate() {
        makeOpen = true;
    }
    void FixedUpdate(){
        if (makeOpen) {
            if (lowerPartStartY - lowerPart.transform.position.y < moveDistance) {
                Vector3 lowerPartPosition = lowerPart.transform.position;
                lowerPartPosition.y = lowerPart.transform.position.y - moveSpeed;
                lowerPart.transform.position = lowerPartPosition;
                
                Vector3 upperPartPosition = upperPart.transform.position;
                upperPartPosition.y = upperPart.transform.position.y + moveSpeed;
                upperPart.transform.position = upperPartPosition;
            }
        }
    } 
}
