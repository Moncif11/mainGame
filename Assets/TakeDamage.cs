using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{

    public float damage ;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player") )
        {
            other.collider.GetComponent<Health>().takeDamage(damage); 
        }
    }
}
