using UnityEngine; 

namespace BehaviorTree{
    public class TaskToUnfreezed : Node
    {
        Animator animator ; 
        Transform _transform; 
        public TaskToUnfreezed(Transform transform){
                animator = transform.GetComponent<Animator>();
                _transform = transform; 
        }
        public override NodeState Evaluate()
        {
            animator.speed = 1; 
            state = NodeState.RUNNING; 
            return state;
            } 
        }
}