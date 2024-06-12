using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbiltyActivater : MonoBehaviour
{
    public Abilty abilty; 
    public void OnTriggerEnter2D(Collider2D other){
        AbilityManager abilityManager = other.GetComponent<AbilityManager>(); 
        if(abilityManager!= null ){
            abilityManager.ability = abilty;
            Destroy(gameObject);
        } 
    }
}
