using BehaviorTree;
using UnityEngine;

public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int  _currentWayPointIndex = 0; 

    public float speed = 2f ;


    public TaskPatrol(Transform transform, Transform[] waypoints){
        _transform = transform;
        _waypoints = waypoints;
    }

    public override NodeState Evaluate()
    {        Debug.Log("Patrolstate : "+ state); 

        Transform wp =_waypoints[_currentWayPointIndex].transform;
        if(Vector2.Distance(wp.position,_transform.position)<0.01f){
            _transform.position = wp.position;   
            _currentWayPointIndex = (_currentWayPointIndex+1) % _waypoints.Length;
            Flip(); 
            isRight(); 
        }
        else{
            _transform.position = Vector2.MoveTowards(_transform.position,wp.position, GuardBT.speed*Time.deltaTime);
        }   
        state = NodeState.RUNNING;
        return state;
    }

    private void Flip(){
        Vector3 Scaler = _transform.localScale;
        Scaler.x*= -1; 
        _transform.localScale = Scaler;
    }

    private void isRight(){
         Vector2 direction = (_transform.position - _waypoints[_currentWayPointIndex].position).normalized; 
         if (direction.x < 0)
        {
            GuardBT.isRight = false; // Nach links schauen
        }
        else if (direction.x > 0)
        {
            GuardBT.isRight = true; // Nach rechts schauen
        }  
    }
}
