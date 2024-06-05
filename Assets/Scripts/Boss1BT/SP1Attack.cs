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
        Vector2 originalVelocity = rigidbody2D.velocity; 
        float speed = Boss1BT.speed*50;
        Vector2 dashVelocity = isLeft ? Vector2.left * speed : Vector2.right * speed;
        rigidbody2D.velocity = dashVelocity;
        //float stopRange = isLeft ? -5 * speed : 5 * speed;
        //Vector2 newPosition = new Vector2(target.position.x+stopRange,target.position.y); 
        //Vector2 direction = Vector2.MoveTowards(transform.position,newPosition, Boss1BT.speed*100*Time.deltaTime);
        //transform.position = new Vector2(direction.x,target.position.y);
        //rigidbody2D.velocity = originalVelocity; 
    }
}
