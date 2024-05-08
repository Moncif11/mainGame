using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using Unity.VisualScripting;
using UnityEngine;

/*
The PlayerController is to control the Actual player with WASD controlling  
*/
public class PlayerController : MonoBehaviour
{
    // movementspeed from the character 
    [SerializeField]private float speed; 
    
     // movementspeed from the character 
    [SerializeField]private float JumpForce; 

    // rigidbody 2d to move the Player
    private new Rigidbody2D rigidbody2D; 

    // boolean for the look direction 
    private bool facingRight = false;

    // boolean to check the Player are under a ground or not  
    bool grounded; 

    //Dash implemented variables 
    private bool canDash = true ;
    private bool isDashing;

    [SerializeField] private float dashingTime = 0.2f;

    [SerializeField] private float dashingCooldown= 1f; 
    [SerializeField] private float  dashsingPower=24f; 

    [SerializeField] private TrailRenderer tr; 
    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {   //grounded = Physics2D.OverlapCircle(groundCheck.position);  
        float moveInput = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(moveInput*speed,rigidbody2D.velocity.y);
        if(Input.GetKey(KeyCode.E) && canDash){
            StartCoroutine(Dash()); 
        }
        if(Input.GetKey(KeyCode.Space)){
            if(grounded){
            rigidbody2D.velocity = Vector2.up*JumpForce;
            grounded = false; 
            }
        }
        if(facingRight==false && moveInput<0){
            Flip();
        }
        else if (facingRight==true && moveInput>0){
             Flip();
        }
    }

    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x*= -1; 
        transform.localScale = Scaler;
    }

    void OnCollisionEnter2D(Collision2D other){
        Debug.Log(other.gameObject.name);
        grounded=true; 
    }

    private IEnumerator Dash(){
        canDash = false; 
        isDashing = true;
        float originalGravity = rigidbody2D.gravityScale ;  
        rigidbody2D.gravityScale = 0f; 
        rigidbody2D.velocity = new Vector2(transform.localScale.x * dashsingPower,0f); 
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        rigidbody2D.gravityScale = originalGravity;
        yield return new WaitForSeconds(dashingCooldown);
        canDash= true;
        isDashing= false;
    }
}
