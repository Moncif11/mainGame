using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;


public class TaskToMeleeAttack : Node
{
    public Transform _transform;
    public Animator _animator;

    GameObject _sword; 
    MonoBehaviour _monobehavior; 

    public TaskToMeleeAttack(Transform transform , GameObject Sword){
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        _monobehavior = transform.GetComponent<MonoBehaviour>();
    }

    public override NodeState Evaluate()
    {   
       _monobehavior.StartCoroutine("MeleeAttack");

        state = NodeState.RUNNING; 
        return state; 
    }

    IEnumerator MeleeAttack(){
        _animator.SetTrigger("Melee1");
        if(_animator.GetCurrentAnimatorClipInfo(0).Length<0){
            yield break; 
        } 
        float remainingTime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        yield return new WaitForSeconds(remainingTime);
        _animator.SetTrigger("Melee2");
        if(_animator.GetCurrentAnimatorClipInfo(0).Length<0){
            yield break; 
        } 
        remainingTime = _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length; 
        yield return new WaitForSeconds(remainingTime);
    }
}
