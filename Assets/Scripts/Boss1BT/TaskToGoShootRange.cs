using BehaviorTree;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boss1AI{
public class TaskToGoShootRange : Node
{
    private Transform _transform;

    Animator animator;  
    public TaskToGoShootRange(Transform transform){
        _transform = transform;
        animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {  
        Transform target = nearestPlayer(); 
        if(target==null){
            state= NodeState.FAILURE;
            return state;
        }
        if(Mathf.Abs(_transform.position.x-target.position.x)>0.01f){
             float stopRange = -(Boss1BT.shootRange-1);
            Boss1BT.isLeft=isLeft(target);
            if(!Boss1BT.isLeft){
                stopRange*=-1; 
            } 
            Vector2 newPosition = new Vector2(target.position.x+stopRange,_transform.position.y);
            animator.ResetTrigger("SP2"); 
            animator.ResetTrigger("Shoot");
            animator.ResetTrigger("Melee");
            animator.ResetTrigger("SP1"); 
            animator.SetTrigger("Walking"); 
            
            Vector2 direction = Vector2.MoveTowards(_transform.position,newPosition, Boss1BT.speed*Time.deltaTime);
            _transform.position = new Vector2(direction.x,_transform.position.y);
        }

        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        Debug.Log("Current State: " + stateInfo.fullPathHash);

        Debug.Log("TaskToGo: Running");
        state = NodeState.RUNNING; 
        return state;         
    }

    private bool isLeft(Transform target){
         Vector2 direction = (_transform.position - target.position).normalized; 
         if (direction.x > 0)
        {
             Vector3 Scaler = _transform.localScale;
             if(Scaler.x <0){
                 Scaler.x = -Scaler.x;
             } 
            _transform.localScale = Scaler;
            return true; 
        }
        else if (direction.x >0)
        {
            Boss1BT.isLeft = false; // Nach rechts schauen
              Vector3 Scaler = _transform.localScale;
            Scaler.x = Mathf.Abs(Scaler.x); 
            _transform.localScale = Scaler;
            return false;
        }  
        return true; 
    }

    private Transform nearestPlayer(){
        float minDistance =0f; 
        Transform targetPlayer; 
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); 
            minDistance = Vector2.Distance(_transform.position,players[0].transform.position); 
            targetPlayer = players[0].transform;
            for(int i = 1; i < players.Length; i++){
            if(Vector2.Distance(_transform.position,players[0].transform.position)<minDistance){
                minDistance = Vector2.Distance(_transform.position,players[0].transform.position); 
                targetPlayer = players[i].transform;
            }
            }
            return targetPlayer;
    }
}
}