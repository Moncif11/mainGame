using System.Collections;
using System.Collections.Generic;
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
    Transform target = FindTarget();
    if (target == null)
    {
        return;
    }

    Vector2 direction = (Vector2)target.position - rb.position;
    direction.Normalize();
    int isRight = direction.x > 0 ? 1 : -1;
    float angleDifference = Vector2.SignedAngle(rb.transform.up, direction);

    float rotateAmount = Mathf.Clamp(angleDifference, -angularSpeed, angularSpeed);
    
    rb.angularVelocity = rotateAmount;

    rb.velocity = isRight* transform.right * speed;
}

    private Transform FindTarget(){
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, 6.0f);
        foreach (Collider2D targetCollider in targets)
        {
            if (targetCollider.gameObject.CompareTag("Player"))
        {
            return targetCollider.gameObject.transform;
        }
        }
        return null;
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
