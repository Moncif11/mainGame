using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class StatusEffectManager : MonoBehaviour
{
    private Health healthScript; 

    public int burnTickTimer = 0;
    // Start is called before the first frame update
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

    IEnumerator Burn(){
        while(burnTickTimer>0){
            burnTickTimer--;
            healthScript.takeDamage(1);
            yield return new WaitForSeconds(0.75f); 
        }
    }
}
