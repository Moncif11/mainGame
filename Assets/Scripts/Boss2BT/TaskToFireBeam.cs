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
            Debug.Log("Beam");
            animator.SetTrigger("Beam");
            Boss2BT.SP2Ready = false; 
            //animator.ResetTrigger("");
            state = NodeState.RUNNING;
            return state;
        }
    }
}
