using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour {
    public float maxHealth;
     public float health = 5;

    public float damageReduction = 0;

    public GameObject dropItem;

    private void Start(){
         health = maxHealth; 
    }
    public void heal(float heal){
        if(health+heal >=maxHealth ){
                health = maxHealth;
        }
        else{
            health+=heal; 
        }
    }
    public void takeDamage(float damage){
        health = health -(damage- damage*(damageReduction/100));
        if(gameObject.CompareTag("Enemy")){
             if(health<= 0){
            if(dropItem!=null){
                    StartCoroutine(itemDropped(transform));
                }
        }
        }       
    }

    IEnumerator itemDropped( Transform transform){
        Debug.Log("Item Dropped");
        yield return new WaitForSeconds(1);
        Debug.Log("2");
        Instantiate(dropItem, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    } 
}
