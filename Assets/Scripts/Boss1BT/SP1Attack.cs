using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SP1Attack : MonoBehaviour
{   
    public Transform target;

    public Boss1BT boss1;  

    public SP1Attack(Transform transform){
        target = transform;
    }

    void SP1(){
        bool isLeft = Boss1BT.isLeft;
        Rigidbody2D rigidbody2D= GetComponent<Rigidbody2D>();
        float speed = Boss1BT.speed*100;
        Vector2 dashVelocity = isLeft ? Vector2.left * speed : Vector2.right * speed;
        rigidbody2D.velocity = dashVelocity; 
    }
}
