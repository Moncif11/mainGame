using BehaviorTree;
using UnityEngine;

namespace HealMonsterAI{
public class TaskToGoDistance : Node
{
    private Transform _transform;

    Animator animator;  
    public TaskToGoDistance(Transform transform){
        _transform = transform;
        animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {  
        Transform target = (Transform)GetData("distance");
//        Debug.Log("Target:" + target.name);
//        Debug.Log("Target Position: "+ target.position);
//        Debug.Log("Runningstate: "+state);  
        if(Vector2.Distance(_transform.position, target.position)>0.01f){
           // Debug.Log("TaskToGo: Success");
            HealMonsterBT.isRight=!isRight(target);
//            Debug.Log("IsRight: " + RangeMonsterBT.isRight);
        float stopRange = 0; 
        if (!HealMonsterBT.isRight) {
            stopRange -= 3;
         } else {
            stopRange += 3; 
        }
           Vector2 newPosition = new Vector2(target.position.x-stopRange,target.position.y); 
            Vector2 direction = Vector2.MoveTowards(_transform.position,newPosition, HealMonsterBT.speed*Time.deltaTime);
            _transform.position = new Vector2(direction.x,_transform.position.y);
        }
        animator.SetTrigger("Running");
        animator.ResetTrigger("Attack");
        state = NodeState.RUNNING; 
        return state;         
    }

    private bool isRight(Transform target){
         Vector2 direction = (_transform.position - target.position).normalized; 
         if (direction.x > 0)
        {
             Vector3 Scaler = _transform.localScale;
             if(Scaler.x >0){
                 Scaler.x = -Scaler.x;
             } 
            _transform.localScale = Scaler;
            return false; 
        }
        else if (direction.x <0)
        {
            HealMonsterBT.isRight = true; // Nach rechts schauen
              Vector3 Scaler = _transform.localScale;
            Scaler.x = Mathf.Abs(Scaler.x); 
            _transform.localScale = Scaler;
            return true;
        }  
        return true; 
    }
    }
}