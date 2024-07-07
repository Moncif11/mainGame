
using UnityEngine;
namespace BehaviorTree{
public class CheckUnfreezed : Node
{    

    Animator  animator;  
    public CheckUnfreezed(Transform transform){
    animator = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if(animator.speed == 0){
            state= NodeState.SUCCESS;
            return state;
        } 
        state = NodeState.FAILURE;
        return state;
    }
}
}