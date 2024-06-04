using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownMangerB1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(Boss1BT.amountShoot<= 0){
            Boss1BT.coolDownSACounter+= Time.deltaTime;
            if(Boss1BT.coolDownSACounter>=Boss1BT.coolDownSA){
                Boss1BT.coolDownSACounter=0;
                Boss1BT.amountShoot = (Boss1BT.bossPhase == BossPhase.ONE) ? 3 : 5;  
            }   
        }
        if(!Boss1BT.SP1Ready){
            Boss1BT.coolDownSP1Counter+= Time.deltaTime;
            if(Boss1BT.coolDownSP1Counter>=Boss1BT.coolDownSP1){
                Boss1BT.coolDownSP1Counter = 0; 
               Boss1BT.SP1Ready = true;  
            }   
        }
        if(!Boss1BT.SP2Ready){
            Boss1BT.coolDownSP2Counter+= Time.deltaTime;
            if(Boss1BT.coolDownSP2Counter>=Boss1BT.coolDownSP2){
                Boss1BT.coolDownSP2Counter = 0;
               Boss1BT.SP2Ready = true;  
            }   
        }
    }
}
