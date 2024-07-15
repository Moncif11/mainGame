using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class WaterBullet : MonoBehaviour
{
   public User user;
   public float damage = 2;
  private void OnTriggerEnter2D(Collider2D other){
    if(user== User.PLAYER){
        if(other.CompareTag("Enemy")||other.CompareTag("Boss")){
        other.GetComponent<Health>()?.takeDamage(damage);
        }
        if(other.CompareTag("Player")) {
            Debug.Log("player hit with players water");
            other.GetComponent<Health>().heal(2);
        }
    }
    else if( user== User.ENEMY ){
         if(other.CompareTag("Player")){
        other.GetComponent<Health>()?.takeDamage(damage); 
        }
        if(other.CompareTag("Enemy")){
            other.GetComponent<Health>().heal(2);
        }
    }   
    Destroy(gameObject);
  }
}
