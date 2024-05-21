using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBullet : MonoBehaviour
{
   public User user;
  private void OnTriggerEnter2D(Collider2D other){
    Debug.Log("Hit");
    if(user== User.PLAYER){
        if(other.CompareTag("Enemy")){
        other.GetComponent<Health>()?.takeDamage(2);
        }
        if(other.CompareTag("Player")){
        if(other.GetComponent<Health>().maxHealth >  other.GetComponent<Health>().health+2){
            other.GetComponent<Health>().health+=2;
            }
        }
    }
    else if( user== User.ENEMY ){
         if(other.CompareTag("Player")){
        other.GetComponent<Health>()?.takeDamage(2); 
        }
        if(other.CompareTag("Enemy")){
        if(other.GetComponent<Health>().maxHealth >  other.GetComponent<Health>().health+2){
            other.GetComponent<Health>().health+=2;
        }
        }
    }   
    Destroy(gameObject);
  }
}
