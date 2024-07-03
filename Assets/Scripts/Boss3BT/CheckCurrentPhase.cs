using BehaviorTree; 
using UnityEngine;

namespace Boss3AI{
public class CheckCurrentPhase : Node
{   
    Transform _transform;
    Health health;

    BossPhase bossPhase;
    public CheckCurrentPhase(Transform transform , BossPhase phase){
        _transform = transform;
        health = transform.GetComponent<Health>();
        bossPhase = phase;

    }

        public override NodeState Evaluate()
        {   
            if(bossPhase == BossPhase.TWO){
                if(Boss3BT.bossPhase == bossPhase ||Boss3BT.bossPhase == BossPhase.THREE ){
                    Debug.Log("BossPhase Two");
                    state = NodeState.SUCCESS; 
                    return state; 
                }
            }
            else if(bossPhase == BossPhase.THREE){
                    if(Boss3BT.bossPhase == bossPhase){
                        Debug.Log("Phase 3");
                    state = NodeState.SUCCESS; 
                    return state; 
                }
            }
            state = NodeState.FAILURE; 
            return state;  
        }
    }
}
