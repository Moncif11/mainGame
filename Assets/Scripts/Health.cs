using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
//using UnityEngine.UIElements;
using UnityEngine.UI;

public class Health : MonoBehaviour {
    public float maxHealth;
     public float health = 5;

    public float damageReduction = 0;
    public GameObject waterItem;
    public GameObject fireItem;
    public GameObject iceItem;
    public GameObject stoneItem;
    public GameObject dropItem;
    public float myid;
    public Image healthBar;

    public void Start(){
         health = maxHealth;
         myid = gameObject.transform.position.x * 1000 + gameObject.transform.position.y;
    }
    public void heal(float heal){
        if(health+heal >=maxHealth ){
                health = maxHealth;
        }
        else{
            health+=heal; 
        }

        if (gameObject.tag == "Player") {
            updateHealthBar();
        }
    }
    public void takeDamage(float damage) {
        health = health -(damage- damage*(damageReduction/100));
        if(gameObject.CompareTag("Enemy")){
            if(health<= 0){
            if(dropItem!=null){
                    StartCoroutine(itemDropped(transform));
                }
                else{
                    Destroy(gameObject); 
                }
        }
        }
        if(gameObject.CompareTag("Boss")){
            if(health<= 0) {
                openBossGate();
            }
        }
        if(gameObject.CompareTag("Player")){
            updateHealthBar();
            if(health<= 0){
                switch (GetComponent<AbilityManager>().ability) {
                    case Abilty.FIRE:
                        dropItem = fireItem;
                        break;
                    case Abilty.ICE:
                        dropItem = iceItem;
                        break;
                    case Abilty.WATER:
                        dropItem = waterItem;
                        break;
                    case Abilty.ROCK:
                        dropItem = stoneItem;
                        break;
                    default:
                        dropItem = null;
                        break;
                }
                GetComponent<AbilityManager>().ability = Abilty.NONE;
                GetComponent<CapePlayerController>().changeCapeToOriginalColor();
                //if(dropItem!=null){
                //    StartCoroutine(itemDroppedFromPlayer(transform));
                //}
                GetComponent<CapePlayerController>().dying();
            }
        }
    }

    IEnumerator itemDropped( Transform transform){
        yield return new WaitForSeconds(1);
        Instantiate(dropItem, transform.position, Quaternion.identity);
        Destroy(gameObject); 
    }
    public void itemDirektDroppedFromPlayer(float xOffset) {
        Abilty ability = GetComponent<AbilityManager>().ability;
        switch (ability) {
            case Abilty.FIRE:
                dropItem = fireItem;
                break;
            case Abilty.ICE:
                dropItem = iceItem;
                break;
            case Abilty.WATER:
                dropItem = waterItem;
                break;
            case Abilty.ROCK:
                dropItem = stoneItem;
                break;
            default:
                dropItem = null;
                break;
        }
        GetComponent<AbilityManager>().ability = Abilty.NONE;
        GetComponent<CapePlayerController>().changeCapeToOriginalColor();
        if(dropItem!=null){
            Vector3 spawnPos = transform.position;
            spawnPos.x = transform.position.x + xOffset;
            Instantiate(dropItem, spawnPos, Quaternion.identity);
        }
    }
    public void updateHealthBar() {
        if (!GetComponent<CapePlayerController>().multiplayer || (GetComponent<CapePlayerController>().multiplayer &&
                                                                  GetComponent<CapePlayerController>().IsOwner)) {
            healthBar.fillAmount = health / maxHealth;
        }
    }
    public void setHealthBar(Image healthbar) {
        healthBar = healthbar;
    }

    public void resetHealth() {
        health = maxHealth;
        updateHealthBar();
    }
    private void openBossGate() {
        List<GameObject> gates = GameObject.FindGameObjectsWithTag ("Gate").ToList();
        gates = gates.ToList().OrderBy(x => x.transform.position.x).ToList();
        GameObject gate = gates[0];
        for (int i = 0; i < gates.Count; i++) {
            if (transform.position.x >= gates[i].transform.position.x) {
                gate = gates[i+1];
            }
        }
        gate.gameObject.GetComponent<GateManager>().openGate();
    }
}
