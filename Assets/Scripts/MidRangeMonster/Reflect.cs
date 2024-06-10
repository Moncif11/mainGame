using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflect : MonoBehaviour
{
    public Rigidbody2D rb; 
    Vector3 lastVelocity ; 
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other){
        if(!other.GetComponent<GameObject>().CompareTag("Bullet")){
            return; 
        }
        Debug.Log("Reflect");
        Collision2D coll = GetComponent<Collision2D>();
            rb =  other.GetComponent<Rigidbody2D>(); 
            lastVelocity = rb.velocity;
            var speed = rb.velocity.magnitude; 
            var direction = Vector3.Reflect(lastVelocity.normalized,coll.contacts[0].normal); 
    }
}
