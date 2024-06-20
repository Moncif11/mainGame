using BehaviorTree;
using UnityEngine;
namespace Boss2AI{
    public class TaskToFireBeam : Node{

        Transform _transform;
        Animator animator; 

        public TaskToFireBeam(Transform transform ){
            _transform = transform;
            animator = transform.GetComponent<Animator>();    
        }

        public override NodeState Evaluate(){
            animator.SetTrigger("Beam");
            //animator.ResetTrigger("");
            state = NodeState.RUNNING;
            return state;
        }
    }
}
