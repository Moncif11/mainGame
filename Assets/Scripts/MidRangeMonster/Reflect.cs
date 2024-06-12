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

void OnCollisionEnter2D(Collision2D collision)
    {   
        Debug.Log("Reflect");
        Rigidbody2D bulletRB = collision.gameObject.GetComponent<Rigidbody2D>();
        if(!collision.gameObject.CompareTag("Bullet")) return; 
        // Prüft, ob das kollidierende Objekt ein Projektil ist
        if (bulletRB != null)
        {
            // Berechnet die neue Richtung basierend auf der Kollisionsnormalen
            Vector2 reflectDirection = Vector2.Reflect(rb.velocity, collision.contacts[0].normal);
            rb.velocity = reflectDirection;
        }
    }
}
