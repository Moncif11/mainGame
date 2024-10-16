using BehaviorTree;
using UnityEngine;
namespace HealMonsterAI{
public class TaskToGo : Node
{
    private Transform _transform;

    Animator animator;  
    public TaskToGo(Transform transform){
        _transform = transform;
        animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {  
        Transform target = (Transform)GetData("target");
//        Debug.Log("Target:" + target.name);
//        Debug.Log("Target Position: "+ target.position);
//        Debug.Log("Runningstate: "+state);  
        if(Vector2.Distance(_transform.position, target.position)>0.01f){
           // Debug.Log("TaskToGo: Success");
             float stopRange = -(HealMonsterBT.attackRange-1);
            HealMonsterBT.isRight=isRight(target);
            if(!HealMonsterBT.isRight){
                stopRange*=-1; 
            }
//            Debug.Log("IsRight: " + RangeMonsterBT.isRight);
            Vector2 newPosition = new Vector2(target.position.x+stopRange,target.position.y-stopRange); 
            Vector2 direction = Vector2.MoveTowards(_transform.position,newPosition, HealMonsterBT.speed*Time.deltaTime);
            _transform.position = new Vector2(direction.x,_transform.position.y);
        }
        animator.SetTrigger("Running");
        animator.ResetTrigger("Attack");
        //Debug.Log("TaskToGo: Running");
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