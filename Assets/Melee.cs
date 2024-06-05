using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public float damage ;
    // Start is called before the first frame update
    void Start()
    {
        damage = Boss1BT.attack;
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player") )
        {
            other.GetComponent<Health>().takeDamage(damage); 
        }
    }
}