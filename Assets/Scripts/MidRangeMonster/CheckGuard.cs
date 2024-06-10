using BehaviorTree;
using UnityEngine; 

namespace MidRangeMonsterAI{
    public class CheckGuard : Node{
        Transform _transform; 
        public CheckGuard(Transform transform){
            _transform = transform;
        }
    }
}