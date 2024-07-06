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
public class AbilityManager : NetworkBehaviour
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
    private GameObject ShieldRock;
 
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
        if (!IsOwner && GetComponent<CapePlayerController>().multiplayer) {
            return;
        }
        if(Input.GetKeyDown(KeyCode.E)){
            Shoot();
        } 
        if(Input.GetKey(KeyCode.E)){
            if(ability == Abilty.ROCK){
                Shield(); 
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
        if(Input.GetKeyDown(KeyCode.Q)){
            if(ultReady){
            switch(ability){
                case Abilty.FIRE:
                Shoot();
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

    private void Shoot() {
        if (GetComponent<CapePlayerController>().multiplayer) {
            if (OwnerClientId == 0) {
                ShootLocal();
                shootClientRpc();
            }
            else {
                ShootLocal();
                shootServerRpc();
            }
            return;
        }
        GameObject bulletPrefab = null;
        switch(ability){
            case Abilty.FIRE:
                bulletPrefab = fireShot;
                break;
            case Abilty.ICE:
                bulletPrefab = iceShot;
                break;
            case Abilty.WATER:
                bulletPrefab = waterShot;
                break;
            default:
                break; 
        }
        if(bulletPrefab == null)
        {
            return;
        }
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

    public void ShootLocal() {
        ability = this.gameObject.GetComponent<AbilityManager>().ability;
        GameObject bulletPrefab = null;
        Debug.Log("ability: "+ability);
        switch(ability){
            case Abilty.FIRE:
                bulletPrefab = fireShot;
                break;
            case Abilty.ICE:
                bulletPrefab = iceShot;
                break;
            case Abilty.WATER:
                bulletPrefab = waterShot;
                break;
            case Abilty.ROCK:
                Shield(); 
                break;
            default:
                break; 
        }
        if(bulletPrefab == null)
        {
            return;
        }
        
        Debug.Log("Initiate BULLET "+bulletPrefab.gameObject.name+" at "+this.transform.position.x);
        if(transform.eulerAngles == new Vector3(0, 0, 0)){
            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
            Quaternion rotation = bullet.transform.rotation;
            rotation *= Quaternion.Euler(0, 0, 90); // Ändere die Rotation um -90 Grad um die Z-Achse
            bullet.transform.rotation = rotation;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
            bulletRB.AddForce(transform.right*1000);
        }
        else{
            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position + transform.right, Quaternion.identity);
            Quaternion rotation = bullet.transform.rotation;
            rotation *= Quaternion.Euler(0, 0, -90); // Ändere die Rotation um -90 Grad um die Z-Achse
            bullet.transform.rotation = rotation;
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
            bulletRB.AddForce(transform.right*1000);
        }
    }

    private void Shield(){

        if (ShieldRock == null) {
            var myResource = Resources.Load("ShieldRock");
            var myPrefab = myResource as GameObject;
            ShieldRock = Instantiate(myPrefab, transform);   
        }

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
        if (ShieldRock != null) {
            Destroy(ShieldRock);
        }
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
    [ServerRpc]
    private void shootServerRpc() {
        Debug.Log("serverrpc");
        
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (!IsOwner) {
                player.GetComponent<AbilityManager>().ShootLocal();
            }
        }
    }
    [ClientRpc]
    private void shootClientRpc() {
        Debug.Log("clientrpc");
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (!IsOwner) {
                player.GetComponent<AbilityManager>().ShootLocal();
            }
        }
    }
}
