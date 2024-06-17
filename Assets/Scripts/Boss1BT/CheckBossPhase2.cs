using UnityEngine;
using BehaviorTree; 

namespace Boss1AI{
public class CheckBossPhase2 : Node
{
    public override NodeState Evaluate(){
        state = (Boss1BT.bossPhase == BossPhase.TWO) ? NodeState.SUCCESS : NodeState.FAILURE; 
        return state; 
    }
}
}