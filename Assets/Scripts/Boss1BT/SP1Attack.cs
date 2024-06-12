using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SP1Attack : MonoBehaviour
{   
    public Transform target;

    public Boss1BT boss1;  
    public Collider2D sword; 

    public SP1Attack(Transform transform){
        target = transform;
    }

    void SP1(){
        
        bool isLeft = Boss1BT.isLeft;
        Rigidbody2D rigidbody2D= GetComponent<Rigidbody2D>();
        Vector2 originalVelocity = rigidbody2D.velocity; 
        float speed = Boss1BT.speed*50;
        sword.enabled = true;
        float damage = sword.GetComponent<Melee>().damage; 
         sword.GetComponent<Melee>().damage = Boss1BT.attack*1.5f; 
        Vector2 dashVelocity = isLeft ? Vector2.left * speed : Vector2.right * speed; 
        //rigidbody2D.velocity = dashVelocity;

        float dashTime = 0.2f; // Duration of the dash in seconds
        float elapsedTime = 0f;
        
        while (elapsedTime < dashTime)
        {
            rigidbody2D.MovePosition(rigidbody2D.position +dashVelocity *  Time.fixedDeltaTime);
            elapsedTime += Time.fixedDeltaTime;
        }
        sword.enabled = false;
        sword.GetComponent<Melee>().damage = damage; 
        Boss1BT.SP1Ready= false; 
        //float stopRange = isLeft ? -5 * speed : 5 * speed;
        //Vector2 newPosition = new Vector2(target.position.x+stopRange,target.position.y); 
        //Vector2 direction = Vector2.MoveTowards(transform.position,newPosition, Boss1BT.speed*100*Time.deltaTime);
        //transform.position = new Vector2(direction.x,target.position.y);
        //rigidbody2D.velocity = originalVelocity; 
    }
}
