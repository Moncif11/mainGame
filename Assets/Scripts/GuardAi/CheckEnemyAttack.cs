using BehaviorTree;
using UnityEngine;

public class CheckEnemyAttack : Node
{   Transform _transform; 
    public CheckEnemyAttack(Transform transform){
    _transform = transform;
    }
    public override NodeState Evaluate()
    {
        object t = GetData("target");
        if(t==null){
            state  = NodeState.FAILURE; 
            return state; 
        }
        Transform target = (Transform) t;
        if(Vector2.Distance(_transform.position, target.position)<= GuardBT.attackRange){            
            state= NodeState.SUCCESS;
            return state;
        } 
        state = NodeState.FAILURE;
        return state;
    }
}
