using BehaviorTree;
using UnityEngine;

namespace Boss2AI{
public class TaskToNextPhase : Node{

    private Transform _transform;

    private BossPhase bossPhase; 
     public TaskToNextPhase(Transform transform, BossPhase bossPhase){
        _transform = transform;
        this.bossPhase = bossPhase;
    }
    public override NodeState Evaluate()
    {  
        Boss2BT.bossPhase = BossPhase.TWO;
        if(this.bossPhase == BossPhase.THREE){
            Boss2BT.bossPhase = BossPhase.THREE;
        }
        Boss2BT.speed *= 1.5f;
        state = NodeState.RUNNING;
        return state;
    }

    }
}