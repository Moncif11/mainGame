using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashBehaviour : MonoBehaviour {
    private String state;
    private float smashDistance;
    public float waitTime = 0.5f;
    private float startY;
    public float downSpeed = 0.2f;
    public float upSpeed = 0.1f;
    void Start()
    {
        state = "ready";
        startY = transform.position.y;
        Transform parent = transform.parent;
        GameObject pillar = parent.Find("Pillar").gameObject;
        smashDistance = pillar.transform.localScale.y-transform.localScale.y/2-1f;
    }

    void FixedUpdate()
    {
        switch (state) {
            case "ready":
                StartCoroutine(waitAndFall());
                state = "preparingToFall";
                break;
            case "falling":
                if (startY - transform.position.y < smashDistance) {
                    Vector3 newPosition = transform.position;
                    newPosition.y = transform.position.y - downSpeed;
                    transform.position = newPosition;
                }
                else {
                    state = "fallen";
                }
                break;
            case "fallen":
                StartCoroutine(waitAndRise());
                state = "preparingToRise";
                break;
            case "rising":
                if (transform.position.y < startY) {
                    Vector3 newPosition = transform.position;
                    newPosition.y = transform.position.y + upSpeed;
                    transform.position = newPosition;
                }
                else {
                    state = "ready";
                }
                break;
        }
    }

    private IEnumerator waitAndFall() {
        yield return new WaitForSeconds(waitTime);
        state = "falling";
    }
    private IEnumerator waitAndRise() {
        yield return new WaitForSeconds(waitTime);
        state = "rising";
    }
}
