using UnityEngine;
using BehaviorTree; 
public class CheckBossPhase2 : Node
{
    public override NodeState Evaluate(){
        state = (Boss1BT.bossPhase == BossPhase.TWO) ? NodeState.SUCCESS : NodeState.FAILURE; 
        return state; 
    }
}