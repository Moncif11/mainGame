using BehaviorTree;
using UnityEngine; 

namespace MidRangeMonsterAI{
    public class TaskToGuard : Node{
        Transform _transform; 
        public TaskToGuard(Transform transform){
            _transform = transform;
        }
    }
    
}