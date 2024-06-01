using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class TaskToSP1 : Node 
{
    Transform _transform;
    Animator _animator;  


    public TaskToSP1(Transform transform){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }
    public override NodeState Evaluate()
    {   
        Transform target = (Transform)GetData("Target");
        Boss1BT.isLeft = IsLeft(target);
        _animator.SetTrigger("SP1");
        _animator.ResetTrigger("Walking");
        _animator.ResetTrigger("Shoot");
        _animator.ResetTrigger("Melee");
        Boss1BT.SP1Ready= false; 
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
