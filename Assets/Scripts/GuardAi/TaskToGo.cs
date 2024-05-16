using BehaviorTree;
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
        Debug.Log("Target Position: "+ target.position);  
        if(Vector2.Distance(_transform.position, target.position)>0.01f){
            Debug.Log("TaskToGo: Success");
             _transform.position = Vector2.MoveTowards(_transform.position,target.position, GuardBT.speed*Time.deltaTime);
        }
        Debug.Log("TaskToGo: Running");
        state = NodeState.RUNNING; 
        return state;  
    }
}