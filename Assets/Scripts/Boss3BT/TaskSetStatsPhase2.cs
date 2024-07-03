using BehaviorTree;
using UnityEngine;

namespace Boss3AI{
public class TaskToNextPhase : Node{

    private Transform _transform;

    private BossPhase bossPhase; 
     public TaskToNextPhase(Transform transform, BossPhase bossPhase){
        _transform = transform;
        this.bossPhase = bossPhase;
    }
    public override NodeState Evaluate()
    {  
        Boss3BT.bossPhase = BossPhase.TWO;
        if(this.bossPhase == BossPhase.THREE){
            Debug.Log("Boss Phase 3");
            Boss3BT.bossPhase = BossPhase.THREE;
        }
        Boss3BT.speed *= 1.5f;
        state = NodeState.RUNNING;
        return state;
    }

    }
}