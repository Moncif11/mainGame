using BehaviorTree;

using UnityEngine;
namespace MidRangeMonsterAI{
public class CheckEnemyAttack : Node
{   Transform _transform; 

    bool isFacing = false;  
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
        Vector2 direction = (_transform.position - target.position).normalized; 
        if(direction.x<0 && RangeMonsterBT.isRight){
            isFacing = true; 
        }
        else if (direction.x>0 && !RangeMonsterBT.isRight){
            isFacing = true;
        }
        else{
            isFacing = false;
        }

        if(Vector2.Distance(_transform.position, target.position)<= RangeMonsterBT.attackRange && isFacing){            
            state= NodeState.SUCCESS;
            return state;
        } 
        state = NodeState.FAILURE;
        return state;
    }
}
}