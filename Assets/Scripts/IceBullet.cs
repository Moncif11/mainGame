using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IceBullet : MonoBehaviour
{
   public User user;
   public float damage = 2; 
  private void OnTriggerEnter2D(Collider2D other){
    Debug.Log("Hit : " + other.gameObject.name);
    if(user== User.PLAYER){
        if(other.CompareTag("Enemy")|| other.CompareTag("Boss")){
        other.GetComponent<Health>()?.takeDamage(damage);
        other.GetComponent<StatusEffectManager>().ApplyFreeze(2); 
        }
    }
    else if( user== User.ENEMY ){
         if(other.CompareTag("Player")){
            other.GetComponent<Health>()?.takeDamage(damage); 
            other.GetComponent<StatusEffectManager>().ApplyFreeze(3,false);
            }
    }   
    Destroy(gameObject);
  }
}
