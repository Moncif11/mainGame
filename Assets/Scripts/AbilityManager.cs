using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Abilty{
    NONE,
    FIRE, 
    ICE,
    WATER, 
    ROCK 
}
public class AbilityManager : MonoBehaviour
{   
    public Abilty abilty;  

    public GameObject fireShot;
    public GameObject fireUlt; 

    public GameObject iceShot; 

    public GameObject waterShot; 

    public float maxUltbar; 
    public float ultBar;

    private CapePlayerController capePlayerController;

    private bool ultReady;  
    // Start is called before the first frame update
    void Start()
    {
        abilty = Abilty.NONE; 
        ultReady = false;    
        capePlayerController = GetComponent<CapePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
        switch(abilty){
            case Abilty.FIRE:
            Shoot(fireShot);
            return;
            case Abilty.ICE:
            Shoot(iceShot);
            return; 
            case Abilty.WATER: 
            Shoot(waterShot);
            return; 
            case Abilty.ROCK:
            Shield(); 
            return;  
            default:
            return; 
        }
        }    

        if(maxUltbar ==ultBar){
            ultReady= true; 
        }    
        if(Input.GetKey(KeyCode.Q)){
            if(ultReady){
            switch(abilty){
                case Abilty.FIRE:
                Shoot(fireUlt);
                return; 
                case Abilty.WATER:
                GetComponent<Health>().takeDamage(-50);
                return; 
                case Abilty.ICE: 
                FreezeAll(); 
                return; 

            }
            ultReady = false; 
            ultBar = 0; 
        }
        }
        
    }

    private void Shoot(GameObject bulletPrefab){
        if(capePlayerController.direction == "left"){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + -transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(transform.right*1000);
            }
        else if(capePlayerController.direction == "right"){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(transform.right*1000);
            }
    }

    private void Shield(){

    }
    private void FreezeAll(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 11.2f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                StatusEffectManager statusEffectManager = collider.GetComponent<StatusEffectManager>();
                if (statusEffectManager != null)
                {
                    statusEffectManager.ApplyFreeze(10,ultReady); 
                }
            }  
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 11.2f);
    }
}
