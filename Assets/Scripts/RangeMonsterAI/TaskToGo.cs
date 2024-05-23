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
        Debug.Log("Target:" + target.name);
        Debug.Log("Target Position: "+ target.position);
        Debug.Log("Runningstate: "+state);  
        if(Vector2.Distance(_transform.position, target.position)>0.01f){
            Debug.Log("TaskToGo: Success");
            Vector2 direction = Vector2.MoveTowards(_transform.position,target.position, RangeMonsterBT.speed*Time.deltaTime);
            _transform.position = new Vector3(direction.x,_transform.position.y,_transform.position.z);
        }
        Debug.Log("TaskToGo: Running");
         state = NodeState.RUNNING; 
         return state;         
    }
}