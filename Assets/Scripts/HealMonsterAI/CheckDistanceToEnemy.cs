using BehaviorTree;

using UnityEngine;
namespace HealMonsterAI{
public class CheckDistanceToEnemy : Node
{   Transform _transform; 

    bool isFacing = false;  
    public CheckDistanceToEnemy(Transform transform){
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
        if(direction.x<0 && HealMonsterBT.isRight){
            isFacing = true; 
        }
        else if (direction.x>0 && !HealMonsterBT.isRight){
            isFacing = true;
        }
        else{
            isFacing = false;
        }

        
        if(Vector2.Distance(_transform.position, target.position)< HealMonsterBT.attackRange-3 && isFacing){
        parent.parent.SetData("distance",(Object)target);
        state= NodeState.SUCCESS;
                return state;
        } 
        state = NodeState.FAILURE;
        return state;
    }   
    }
}