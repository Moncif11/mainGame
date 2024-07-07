
using UnityEngine;
namespace BehaviorTree{
public class CheckFreezed : Node
{   Transform _transform; 
    Rigidbody2D rigidbody2D; 

    Animator  animator;  
    public CheckFreezed(Transform transform){
    _transform = transform;
    rigidbody2D = transform.GetComponent<Rigidbody2D>();
    animator = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        if(AreAllConstraintsFrozen(rigidbody2D)){
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