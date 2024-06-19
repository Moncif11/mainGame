using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.UIElements;

public class HomingMissile : MonoBehaviour
{
     Rigidbody2D rb; 

     public float speed = 10f; 
    public float angularSpeed = 200f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform target = FindTarget (); 
        if(target == null){
            return;
        }
        Vector2 direction = (Vector2) target.position - rb.position; 
        direction.Normalize(); 
        float rotateAmount= Vector3.Cross(-direction,transform.up).z;
        rb.angularVelocity = rotateAmount * angularSpeed;
        rb.velocity = transform.up*speed;
    }

    private Transform FindTarget(){
         GameObject target = null; 
         Collider2D[] targets= Physics2D.OverlapCircleAll(transform.position, 3.0f);  
         for (int i = 0; i< targets.Length; i++) {
                if(targets[i].gameObject.CompareTag("Player")) {
                        target = targets[i].gameObject;
                }  
            }
            if(target != null) {
                return null;
            }
    return target.transform;  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player")){
            Health playerHealth = other.GetComponent<Health>();
            playerHealth.takeDamage(5); 
        }
            
        // Zerstöre dieses GameObject
        Destroy(gameObject);
    }
}
