
using UnityEngine;
namespace BehaviorTree{
public class CheckUnfreezed : Node
{    

    Animator  animator;  
    Rigidbody2D rb; 
    public CheckUnfreezed(Transform transform){
    animator = transform.GetComponent<Animator>();
    rb = transform.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        if(animator.speed == 0 && !AreAllConstraintsFrozen(rb)){
            state= NodeState.SUCCESS;
            return state;
        } 
        state = NodeState.FAILURE;
        return state;
    }

     private bool AreAllConstraintsFrozen(Rigidbody2D rb)
    {
        // Definiere die erwarteten Constraints, wenn alle gefreezed sind
        RigidbodyConstraints2D allFrozen = RigidbodyConstraints2D.FreezePositionX |
                                           RigidbodyConstraints2D.FreezePositionY |
                                           RigidbodyConstraints2D.FreezeRotation;

        // Überprüfe, ob die aktuellen Constraints alle erwarteten Constraints enthalten
        return (rb.constraints & allFrozen) == allFrozen;
    }
}
}