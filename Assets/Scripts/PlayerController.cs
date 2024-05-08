using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/*
The PlayerController is to control the Actual player with WASD controlling  
*/
public class PlayerController : MonoBehaviour
{
    // movementspeed from the character 
    [SerializeField]private float speed; 
    private Rigidbody2D rigidbody2D; 

    bool grounded; 
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,rigidbody2D.velocity.y);
    }
}
