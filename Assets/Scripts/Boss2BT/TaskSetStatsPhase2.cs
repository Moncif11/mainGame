using BehaviorTree;
using UnityEngine;

namespace Boss2AI{
public class TaskToNextPhase : Node{

    Transform _transform;

    BossPhase bossPhase; 
     public TaskToNextPhase(Transform transform, BossPhase bossPhase){
        _transform = transform;
        this.bossPhase = bossPhase;
    }
    public override NodeState Evaluate()
    {   Debug.Log("Boss Phase 2");
        Boss2BT.bossPhase = BossPhase.TWO;
        if(bossPhase == BossPhase.TWO){
            Boss2BT.bossPhase = BossPhase.THREE;
        }
        Boss2BT.speed *= 1.5f;
        state = NodeState.RUNNING;
        return state;
    }

    }
}