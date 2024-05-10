using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {
     public float health = 5; 
    public void takeDamage(float damage){
        health-=damage; 
        if(health<= 0){
            Destroy(gameObject); 
        }
    }
}