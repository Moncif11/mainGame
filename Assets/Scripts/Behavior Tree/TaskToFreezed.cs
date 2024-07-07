using UnityEngine; 

namespace BehaviorTree{
    public class TaskToFreezed : Node
    {
        Animator animator ; 
        Transform _transform; 
        public TaskToFreezed(Transform transform){
                animator = transform.GetComponent<Animator>();
                _transform = transform; 
        }
        public override NodeState Evaluate()
        {

            _transform.position = Vector2.MoveTowards(_transform.position,_transform.position,0); 
            animator.speed = 0; 
            state = NodeState.RUNNING; 
            return state;
            } 
        }
}