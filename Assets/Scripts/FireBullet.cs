using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum User{
    PLAYER, 
    ENEMY,
}
public class FireBullet : MonoBehaviour
{
   public User user;
  private void OnTriggerEnter2D(Collider2D other){
    Debug.Log("Hit: " + other.gameObject.name);
    if(user== User.PLAYER){
        if(other.CompareTag("Enemy")){
        other.GetComponent<Health>()?.takeDamage(2);
        if(other.GetComponent<StatusEffectManager>() != null){
        other.GetComponent<StatusEffectManager>().ApplyBurn(4); 
        }
        }
    }
    else if( user== User.ENEMY ){
         if(other.CompareTag("Player")){
        other.GetComponent<Health>()?.takeDamage(2);
        if(other.GetComponent<StatusEffectManager>() != null){
        other.GetComponent<StatusEffectManager>().ApplyBurn(4); 
        } 
      } 
    }   
    Destroy(gameObject);
  }

}
