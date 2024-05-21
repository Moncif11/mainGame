using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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

    public void ApplyFreeze(int ticks){
        freezeTicktimer = ticks;
         StartCoroutine(Freeze());
    }

    IEnumerator Burn(){
        while(burnTickTimer>0){
            burnTickTimer--;
            healthScript.takeDamage(1);
            yield return new WaitForSeconds(0.75f); 
        }
    }

    IEnumerator Freeze(){
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(freezeTicktimer);  
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;   
    
    }
}
