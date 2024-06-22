using UnityEngine;
using BehaviorTree; 

namespace Boss2AI{
public class CheckBossPhase2 : Node
{
    public override NodeState Evaluate(){
        state = (Boss2BT.bossPhase == BossPhase.TWO || Boss1BT.bossPhase == BossPhase.THREE) ? NodeState.SUCCESS : NodeState.FAILURE; 
        return state; 
    }
}
}