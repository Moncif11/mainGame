using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

public class TaskToSP2 : Node
{
    Transform _transform; 
    Animator _animator;
    
    public TaskToSP2(Transform transform){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Boss1BT.isLeft = IsLeft(target);
        Debug.Log("SP2 activated");
        _animator.SetTrigger("SP2");
        _animator.ResetTrigger("Walking");
        _animator.ResetTrigger("Shoot");
        _animator.ResetTrigger("Melee");
        _animator.ResetTrigger("SP1");
        Boss1BT.SP2Ready= false; 
        state = NodeState.RUNNING; 
        return state;
    }

    private bool IsLeft(Transform target)
    {
        Vector2 direction = (_transform.position - target.position).normalized;
        Vector3 scaler = _transform.localScale;

        if (direction.x > 0)
        {
            if (scaler.x < 0)
            {
                scaler.x = -scaler.x;
            }
            _transform.localScale = scaler;
            return true;
        }
        else if (direction.x < 0)
        {
            if (scaler.x > 0)
            {
                scaler.x = -scaler.x;
            }
            _transform.localScale = scaler;
            return false;
        }
        return true;
    }
}
