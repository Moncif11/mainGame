using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float maxHealth;
     public float health = 5;

    public GameObject dropItem;

    private void Start(){
         health = maxHealth; 
    }
    public void takeDamage(float damage){
        health-=damage;
        if(gameObject.CompareTag("Enemy")){
             if(health<= 0){
            Destroy(gameObject);
            if(dropItem!=null){
                    Instantiate(dropItem, transform.position, Quaternion.identity);
                } 
        }
        } 
       
    }
}