using BehaviorTree;
using UnityEngine;

namespace Boss3AI{
public class CheckBeginPhase : Node
{   
    Transform _transform;
    Health health;

    BossPhase bossPhase;
    public CheckBeginPhase(Transform transform , BossPhase phase){
        _transform = transform;
        health = transform.GetComponent<Health>();
        bossPhase = phase;

    }

        public override NodeState Evaluate()
        {   
             float currentHP = health.health;  
             float HPThreshhold = (health.maxHealth/3)* 2; 
             if(bossPhase == BossPhase.TWO){
                 HPThreshhold = health.maxHealth/3;
             }               
            if( HPThreshhold >= currentHP && bossPhase == Boss3BT.bossPhase){
                Debug.Log("Begin new Phase");
                state = NodeState.SUCCESS; 
               return state;  
            }
            state = NodeState.FAILURE; 
            return state;  
        }
    }
}