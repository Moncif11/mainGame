using BehaviorTree;
using UnityEngine;
namespace HealMonsterAI{
public class TaskPatrol : Node
{
    private Transform _transform;
    private Transform[] _waypoints;

    private int  _currentWayPointIndex = 0; 

    public float speed = 2f ;

    Animator animator;

    public TaskPatrol(Transform transform, Transform[] waypoints){
        _transform = transform;
        _waypoints = waypoints;
        animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {        

        Transform wp =_waypoints[_currentWayPointIndex].transform;
        if(Vector2.Distance(wp.position,_transform.position)<0.01f){
            _transform.position = wp.position;   
            _currentWayPointIndex = (_currentWayPointIndex+1) % _waypoints.Length;
            //state = NodeState.SUCCESS;
        }
        else{
            _transform.position = Vector2.MoveTowards(_transform.position,wp.position, HealMonsterBT.speed*Time.deltaTime);
        }   
         isRight();
         Flip();
        animator.SetTrigger("Running");
        animator.ResetTrigger("Attack");
        state = NodeState.RUNNING;
        return state;
    }

    private void Flip(){
        if(HealMonsterBT.isRight){
            Vector3 Scaler = _transform.localScale;
            if(Scaler.x >0){
                Scaler.x = -Scaler.x;
            } 
            _transform.localScale = Scaler;
        }
        else{
            Vector3 Scaler = _transform.localScale;
            Scaler.x = Mathf.Abs(Scaler.x); 
            _transform.localScale = Scaler;
        }
    }

    private void isRight(){
         Vector2 direction = (_transform.position - _waypoints[_currentWayPointIndex].position).normalized; 
         if (direction.x < 0)
        {
            HealMonsterBT.isRight = false; // Nach links schauen
        }
        else if (direction.x > 0)
        {
            HealMonsterBT.isRight = true; // Nach rechts schauen
        }  
    }
    }
}