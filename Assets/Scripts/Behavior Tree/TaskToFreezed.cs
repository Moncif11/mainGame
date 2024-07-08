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
            float stayPositionx = _transform.position.x; 
            float stayPositiony = _transform.position.y;
            _transform.position = Vector3.MoveTowards(_transform.position,_transform.position,0);
            animator.speed = 0; 
            Debug.Log("TaskFreezed"); 
            state = NodeState.RUNNING; 
            return state;
            } 
        }
}