using BehaviorTree;
using UnityEngine; 

namespace MidRangeMonsterAI{
    public class TaskToGuard : Node{
        Transform _transform; 

        Animator _animator; 
        public TaskToGuard(Transform transform){
            
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
        }

        public override NodeState Evaluate()
        {   
            _animator.SetTrigger("Guard");
            _animator.ResetTrigger("Attack");
            _animator.ResetTrigger("Running");
            state = NodeState.RUNNING; 
            return state;
        }
    }
    
}