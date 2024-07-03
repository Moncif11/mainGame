using UnityEngine;
using BehaviorTree; 

namespace Boss3AI{
public class CheckBossPhase2 : Node
{
    public override NodeState Evaluate(){
        state = (Boss3BT.bossPhase == BossPhase.TWO || Boss3BT.bossPhase == BossPhase.THREE) ? NodeState.SUCCESS : NodeState.FAILURE; 
        return state; 
    }
}
}