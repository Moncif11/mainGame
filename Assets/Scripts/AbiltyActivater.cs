using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AbiltyActivater : MonoBehaviour
{
    public Abilty abilty; 
    public void OnTriggerEnter2D(Collider2D other){
        Debug.Log("Ability "+abilty+" triggered!");
        if(!other.gameObject.CompareTag("Player")){return;}
        AbilityManager abilityManager = other.GetComponent<AbilityManager>();
        if(abilityManager!= null ){
            abilityManager.ability = abilty;
            switch(abilty) 
            {
                case Abilty.WATER:
                    other.GetComponent<CapePlayerController>().changeCapeColor(22, 7, 110);
                    break;
                case Abilty.ICE:
                    other.GetComponent<CapePlayerController>().changeCapeColor(5, 161, 181);
                    break;
                case Abilty.FIRE:
                    other.GetComponent<CapePlayerController>().changeCapeColor(214, 73, 2);
                    break;
                case Abilty.ROCK:
                    other.GetComponent<CapePlayerController>().changeCapeColor(87, 32, 5);
                    break;
                default:
                    other.GetComponent<CapePlayerController>().changeCapeColor(123, 0, 22);
                    break;
            }
            Destroy(gameObject);
        } 
    }
}
