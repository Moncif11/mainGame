
using BehaviorTree;
using UnityEngine;

namespace Boss2AI{
public class TaskToGoToAttackRange : Node
{
    Transform _transform; 
    Animator _animator; 

    public TaskToGoToAttackRange(Transform transform){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {   
        Transform target = (Transform)GetData("target");
        if(Vector2.Distance(_transform.position, target.position)>0.01f){
             float stopRange = -(Boss1BT.meleeRange-1);
            Boss1BT.isLeft=isLeft(target);
            if(!Boss1BT.isLeft){
                stopRange*=-1; 
            }
            _animator.ResetTrigger("SP2");
            _animator.ResetTrigger("Shoot");
            _animator.ResetTrigger("Melee");
            _animator.ResetTrigger("SP1"); 
            _animator.SetTrigger("Walking"); 
            Vector2 newPosition = new Vector2(target.position.x+stopRange,target.position.y-stopRange); 
            Vector2 direction = Vector2.MoveTowards(_transform.position,newPosition, Boss1BT.speed*Time.deltaTime);
            _transform.position = new Vector2(direction.x,_transform.position.y);
        } 
        state = NodeState.RUNNING; 
        return state; 
    }
   private bool isLeft(Transform target) {
    Vector2 direction = (_transform.position - target.position).normalized;
    if (direction.x > 0) {
        Boss2BT.isLeft = true;
        _transform.Rotate(0, 0, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    } else if (direction.x < 0) {  // Change to "< 0" for the correct check
        Boss2BT.isLeft = true;
        _transform.Rotate(0, 180, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    }
    return true;
    }
}
}