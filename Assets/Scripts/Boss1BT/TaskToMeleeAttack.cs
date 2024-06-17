using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

namespace Boss1AI{
public class TaskToMeleeAttack : Node
{
    public Transform _transform;
    public Animator _animator;

    GameObject _sword; 
    public TaskToMeleeAttack(Transform transform /*, GameObject Sword*/){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {   
        Debug.Log("Melee : Attacking");
        _animator.SetTrigger("Melee");
        _animator.ResetTrigger("Walking");
        _animator.ResetTrigger("Shoot");
        _animator.ResetTrigger("SP1");
        _animator.ResetTrigger("SP2"); 
        state = NodeState.RUNNING; 
        return state; 
    }

}
}