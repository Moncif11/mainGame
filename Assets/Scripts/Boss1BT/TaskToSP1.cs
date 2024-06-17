using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

namespace Boss1AI{
public class TaskToSP1 : Node 
{
    Transform _transform;
    Animator _animator;  

    SP1Attack _SP1Attack; 

    public TaskToSP1(Transform transform){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _SP1Attack= transform.GetComponent<SP1Attack>();
    }
    public override NodeState Evaluate()
    {   
        Transform target = (Transform)GetData("target");
        Debug.Log("Target ? " + target);
        Boss1BT.isLeft = IsLeft(target);
        //_SP1Attack.target = target;

        Debug.Log("SP1 activated");
        _animator.SetTrigger("SP1");
        _animator.ResetTrigger("SP2");
        _animator.ResetTrigger("Walking");
        _animator.ResetTrigger("Shoot");
        _animator.ResetTrigger("Melee");
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
}