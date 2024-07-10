using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public float damage = 10 ;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player") )
        {   
            FindObjectOfType<AudioManager>().Play("Melee"); 
            other.GetComponent<Health>().takeDamage(damage); 
        }
    }
}
