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
            state = NodeState.RUNNING; 
            return state;
        }
    }
    
}