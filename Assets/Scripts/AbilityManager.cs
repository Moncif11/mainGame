using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
    public Abilty ability;  

    public GameObject fireShot;
    public GameObject fireUlt; 

    public GameObject iceShot; 

    public GameObject waterShot; 

    public float maxUltbar; 
    public float ultBar;

    private CapePlayerController capePlayerController;

    private bool ultReady;  

    private bool ultRunning; 
    private bool ShieldActive = false ; 
 
    // Start is called before the first frame update
    void Start()
    {
        ability = Abilty.NONE; 
        ultReady = false;    
        capePlayerController = GetComponent<CapePlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)){
        switch(ability){
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
        else{
            if(ability ==Abilty.ROCK){
                 DeactivateShield(); 
            }
        }
        if(maxUltbar ==ultBar){
            ultReady= true; 
        }    
        if(Input.GetKey(KeyCode.Q)){
            if(ultReady){
            switch(ability){
                case Abilty.FIRE:
                Shoot(fireUlt);
                return; 
                case Abilty.WATER:
                GetComponent<Health>().heal(50);
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
                GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, -90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(transform.right*1000);
            }
        else if(capePlayerController.direction == "right"){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, 90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(transform.right*1000);
            }
    }

    private void Shield(){
        if(ultRunning == true)return; 
        Health health = GetComponent<Health>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        health.damageReduction = 100;
        float tickTimer = Time.deltaTime;  
        int tickCounter = 0; 
        ShieldActive =true; 
        while(tickCounter >10 && ShieldActive){
            rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;
            tickTimer+=Time.deltaTime;
            if(tickTimer >= 1){
             health.damageReduction-= 10;   
            }
        }
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;  
        return; 
    }

        private void DeactivateShield(){
            if(ultRunning)return; 
        Health health = GetComponent<Health>();
        Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
        health.damageReduction = 0;
        ShieldActive = false; 
        rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;  
        return; 
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

     private void Instructable()
    {
        StartCoroutine(UltRoutine());
    }

    private IEnumerator UltRoutine()
    {
        Health health= GetComponent<Health>();
        health.damageReduction = 100;
        ultRunning = true;

        yield return new WaitForSeconds(10);

        health.damageReduction = 0; // oder setze den Schaden wieder auf den ursprünglichen Wert zurück
        ultRunning = false;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 11.2f);
    }
}
