using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private Health healthScript; 

    public int burnTickTimer = 0;
    // Start is called before the first frame update
    public int freezeTicktimer = 0;
    void Start()
    {
        healthScript = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyBurn(int ticks){
        burnTickTimer = ticks; 
        StartCoroutine(Burn()); 
    }

    public void ApplyFreeze(int ticks , bool isUlt=false)
    {   
        if(!isUlt!|| freezeTicktimer == 0){
        freezeTicktimer = ticks;
        }
         StartCoroutine(Freeze());
    }

    IEnumerator Burn(){
        var myResource = Resources.Load("BurnParticles");
        var myPrefab = myResource as GameObject;
        var myGameObject = Instantiate(myPrefab, transform);
        if (this.gameObject.tag == "Player") {
            Vector3 changedPosition = transform.position;
            changedPosition.y = transform.position.y - 0.4f;
            myGameObject.transform.position = changedPosition;
        }
        while(burnTickTimer>0){
            burnTickTimer--;
            healthScript.takeDamage(1);
            yield return new WaitForSeconds(0.75f);
        }
        Destroy(myGameObject);
    }

    IEnumerator Freeze(){
        var myResource = Resources.Load("FreezeCrystal");
        var myPrefab = myResource as GameObject;
        var myGameObject = Instantiate(myPrefab, transform);
        
        if (this.gameObject.tag == "Enemy") {
            Vector3 changedSize = myGameObject.transform.localScale;
            changedSize = myGameObject.transform.localScale * 0.5f;
            myGameObject.transform.localScale = changedSize;
        }
        else {
            
        }
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
         rb.velocity = Vector2.zero; 
        yield return new WaitForSeconds(freezeTicktimer);  
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;   
        
        Destroy(myGameObject);
    }
}
