using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

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
    private bool isLeft(Transform target){
         Vector2 direction = (_transform.position - target.position).normalized; 
         if (direction.x > 0)
        {
             Vector3 Scaler = _transform.localScale;
             if(Scaler.x <0){
                 Scaler.x = -Scaler.x;
             } 
            _transform.localScale = Scaler;
            return true; 
        }
        else if (direction.x >0)
        {
            Boss1BT.isLeft = false; // Nach rechts schauen
              Vector3 Scaler = _transform.localScale;
            Scaler.x = Mathf.Abs(Scaler.x); 
            _transform.localScale = Scaler;
            return false;
        }  
        return true; 
    }
}
