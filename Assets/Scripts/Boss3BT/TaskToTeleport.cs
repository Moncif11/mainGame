using UnityEngine;
using BehaviorTree;


namespace Boss3AI{
public class TaskToTeleport : Node
{private Transform _transform;
        Animator animator; 
        Vector2 position; 
    public TaskToTeleport(Transform transform){
        _transform = transform;
        position = transform.position;
        animator = transform.GetComponent<Animator>();
    }
        public override NodeState Evaluate(){
            Transform target = (Transform)GetData("target"); 
            float offSet = -2;
            animator.ResetTrigger("Melee");
            animator.ResetTrigger("Walking"); 
            animator.ResetTrigger("Nothing"); 
            animator.ResetTrigger("Burn"); 
            animator.SetTrigger("Teleport"); 
            animator.ResetTrigger("Ice Rain"); 
            GameObject.FindObjectOfType<AudioManager>().Play("Boss3 Teleport");     
            if(Vector2.Distance( position,new Vector2(target.position.x+offSet,target.position.y)) < 25){
                _transform.position  = new Vector2(target.position.x+offSet,_transform.position.y); 
            }
            else{
                 _transform.position  = new Vector2(target.position.x-offSet,_transform.position.y); 
            }
            Boss3BT.isLeft = isLeft(target); 
            Debug.Log("Teleport"); 
            Boss3BT.teleportReady = false;
            state = NodeState.RUNNING;
            return state;
        }

        private bool isLeft(Transform target) {
    Vector2 direction = (_transform.position - target.position).normalized;
    if (direction.x > 0) {
        Boss2BT.isLeft = true;
        _transform.rotation = Quaternion.Euler(0, 0, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    } else if (direction.x < 0) {  // Change to "< 0" for the correct check
        Boss2BT.isLeft = true;
        _transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate around the Y-axis by 180 degrees
        return false;
    }
    return true;
    }
    }

}