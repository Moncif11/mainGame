using BehaviorTree; 
using UnityEngine;

namespace Boss2AI{
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
                if(Boss2BT.bossPhase == bossPhase ||Boss2BT.bossPhase == BossPhase.THREE ){
                    Debug.Log("BossPhase Two");
                    state = NodeState.SUCCESS; 
                    return state; 
                }
            }
            else if(bossPhase == BossPhase.THREE){
                    if(Boss2BT.bossPhase == bossPhase){
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
