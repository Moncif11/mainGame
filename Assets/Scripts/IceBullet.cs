using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class IceBullet : MonoBehaviour
{
   public User user;
  private void OnTriggerEnter2D(Collider2D other){
    Debug.Log("Hit");
    if(user== User.PLAYER){
        if(other.CompareTag("Enemy")){
        other.GetComponent<Health>()?.takeDamage(2);
        other.GetComponent<StatusEffectManager>().ApplyFreeze(2); 
        }
    }
    else if( user== User.ENEMY ){
         if(other.CompareTag("Player")){
            other.GetComponent<Health>()?.takeDamage(2); 
            other.GetComponent<StatusEffectManager>().ApplyFreeze(3);
            }
    }   
    Destroy(gameObject);
  }
}
