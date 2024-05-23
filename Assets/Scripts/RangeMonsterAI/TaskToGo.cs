using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine;

public class TaskToGo : Node
{
    private Transform _transform; 
    public TaskToGo(Transform transform){
        _transform = transform;
    }

    public override NodeState Evaluate()
    {  
        Transform target = (Transform)GetData("target");
//        Debug.Log("Target:" + target.name);
//        Debug.Log("Target Position: "+ target.position);
//        Debug.Log("Runningstate: "+state);  
        if(Vector2.Distance(_transform.position, target.position)>0.01f){
           // Debug.Log("TaskToGo: Success");
             float stopRange = -(RangeMonsterBT.attackRange-1);
            RangeMonsterBT.isRight=isRight(target);
            if(!RangeMonsterBT.isRight){
                stopRange*=-1; 
            }
//            Debug.Log("IsRight: " + RangeMonsterBT.isRight);
            Vector2 newPosition = new Vector2(target.position.x+stopRange,target.position.y-stopRange); 
            Vector2 direction = Vector2.MoveTowards(_transform.position,newPosition, RangeMonsterBT.speed*Time.deltaTime);
            _transform.position = new Vector2(direction.x,_transform.position.y);
        }
        Debug.Log("TaskToGo: Running");
        state = NodeState.RUNNING; 
        return state;         
    }

    private bool isRight(Transform target){
         Vector2 direction = (_transform.position - target.position).normalized; 
         if (direction.x > 0)
        {
             Vector3 Scaler = _transform.localScale;
            Scaler.x = -1; 
            _transform.localScale = Scaler;
            return false; 
        }
        else if (direction.x <0)
        {
            RangeMonsterBT.isRight = true; // Nach rechts schauen
              Vector3 Scaler = _transform.localScale;
            Scaler.x = 1; 
            _transform.localScale = Scaler;
            return true;
        }  
        return true; 
    }
}