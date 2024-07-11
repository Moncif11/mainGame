using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

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
    
    private Coroutine shieldCoroutine;

      public Image ultBarImage;
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
        updateUltBar();
        if ((!GetComponent<CapePlayerController>().IsOwner && GetComponent<CapePlayerController>().multiplayer) || ability == Abilty.NONE) {
            return;
        }
        if(Input.GetKeyDown(KeyCode.E) && ability != Abilty.ROCK){
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
        if(Input.GetKeyDown(KeyCode.R)){
            if(ultReady){
            switch(ability){
                case Abilty.FIRE:
                ultRunning = true; 
                Shoot();
                ultRunning = false; 
                break; 
                case Abilty.WATER:
                GetComponent<Health>().heal(50);
                break; 
                case Abilty.ICE: 
                FreezeAll(); 
                break; 
                case Abilty.ROCK: 
                Instructable();
                break; 
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
        if(ultBar<maxUltbar){
        ultBar++;
        }
        switch(ability){
            case Abilty.FIRE:
                bulletPrefab = fireShot;
                if(ultRunning) bulletPrefab = fireUlt;
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
                 if(ultRunning) bulletPrefab = fireUlt;                
                break;
            case Abilty.ICE:
                bulletPrefab = iceShot;
                break;
            case Abilty.WATER:
                bulletPrefab = waterShot;
                break;
            case Abilty.ROCK:
                if (GetComponent<CapePlayerController>().multiplayer) {
                    Shield(); 
                }
                break;
            default:
                break; 
        }
        if(bulletPrefab == null)
        {
            return;
        }
        
        Debug.Log("Initiate BULLET "+bulletPrefab.gameObject.name+" at "+this.transform.position.x);
         FindObjectOfType<AudioManager>().Play("Shoot"); 
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
        var myPrefab = Resources.Load<GameObject>("ShieldRock");
        ShieldRock = Instantiate(myPrefab, transform);   
    }

    if(ultRunning == true || ShieldActive) return;

    Health health = GetComponent<Health>();
    Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();

    health.damageReduction = 100;
    ShieldActive = true;

    if (shieldCoroutine != null) {
        StopCoroutine(shieldCoroutine);
    }
    shieldCoroutine = StartCoroutine(ShieldCoroutine(health, rigidbody2D));
}

private IEnumerator ShieldCoroutine(Health health, Rigidbody2D rigidbody2D){
    rigidbody2D.constraints = RigidbodyConstraints2D.FreezeAll;

    while (ShieldActive && health.damageReduction > 0) {
    yield return new WaitForSeconds(1);
    health.damageReduction -= 10;
    ultBar++;
    Debug.Log("Losing DamageReduction");
    if (health.damageReduction <= 0) {
        health.damageReduction = 0;
        ShieldActive = false;
    }
}
    ResetShield(health, rigidbody2D);
} 
private void ResetShield(Health health, Rigidbody2D rigidbody2D) {
    health.damageReduction = 0;
    rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    ShieldActive = false;
    }
    private void DeactivateShield(){
    if (shieldCoroutine != null) {
        StopCoroutine(shieldCoroutine);
        shieldCoroutine = null;
    }
    if(ShieldRock!=null){
        Destroy(ShieldRock);
    }
    Health health = GetComponent<Health>();
    Rigidbody2D rigidbody2D = GetComponent<Rigidbody2D>();
    ResetShield(health, rigidbody2D);
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
         if (ShieldRock == null) {
        var myPrefab = Resources.Load<GameObject>("ShieldRock");
        ShieldRock = Instantiate(myPrefab, transform);   
        }
        Health health= GetComponent<Health>();
        health.damageReduction = 100;
        ultRunning = true;
        Debug.Log("Instructable");
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
            GameObject Head = player.transform.GetChild(3).gameObject;
            var playerColor = Head.GetComponent<SpriteRenderer>().color;
            
            if (playerColor == new Color32(0, 255, 0, 255)) {
                Debug.Log("server received, not owner shoots player x: "+player.transform.position.x);
                player.GetComponent<AbilityManager>().ShootLocal();
            }
        }
    }
    [ClientRpc]
    private void shootClientRpc() {
        if (IsServer) {
            return;
        }
        Debug.Log("clientrpc");
        GameObject[] players;
        players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            GameObject Head = player.transform.GetChild(3).gameObject;
            var playerColor = Head.GetComponent<SpriteRenderer>().color;
            
            if (playerColor == new Color32(0, 255, 0, 255)) {
                Debug.Log("client received, not owner shoots player x: "+player.transform.position.x);
                player.GetComponent<AbilityManager>().ShootLocal();
            }
        }
    }

    public void updateUltBar() {
        if (!GetComponent<CapePlayerController>().multiplayer || (GetComponent<CapePlayerController>().multiplayer &&
                                                                  GetComponent<CapePlayerController>().IsOwner)) {
            ultBarImage.fillAmount = ultBar /maxUltbar ;
        }
    }
    public void setUltBar(Image ultBar) {
        ultBarImage = ultBar;
    }

    public void resetUlt() {
        ultBar = 0;
        updateUltBar();
    }
}
