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
    
    float attackCounter = 0; 
    float attackThreshold = 1f;
    public TaskToMeleeAttack(Transform transform /*, GameObject Sword*/){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {   
        attackCounter+=Time.deltaTime;
        if(attackCounter>= attackThreshold){
        GameObject.FindObjectOfType<AudioManager>().Play("Melee"); 
        Debug.Log("Melee : Attacking");
        _animator.SetTrigger("Melee");
        _animator.ResetTrigger("Walking");
        _animator.ResetTrigger("Shoot");
        _animator.ResetTrigger("SP1");
        _animator.ResetTrigger("SP2");
        attackCounter = 0;  
        }
        state = NodeState.RUNNING; 
        return state; 
    }

}
}