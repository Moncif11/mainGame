using BehaviorTree;
using UnityEngine;

namespace Boss3AI{
public class CheckToGoToAttackRange : Node{
    Transform _transform;

    public CheckToGoToAttackRange(Transform transform){
        _transform = transform;
    }

    public override NodeState Evaluate()
    {  
        Transform target = nearestPlayer();
        if(target==null){
            state= NodeState.FAILURE;
            return state;
        }
        float distance = Mathf.Abs(_transform.position.x-target.position.x);    
        if(distance > Boss3BT.attackRange){
            parent.parent.parent.parent.SetData("target",(Object)target);
            state = NodeState.SUCCESS; 
            return state; 
        } 
        state = NodeState.FAILURE; 
        return state;
    }

private bool isLeft(Transform target) {
    Vector2 direction = (_transform.position - target.position).normalized;
    if (direction.x > 0) {
        _transform.rotation= Quaternion.Euler(0, 0, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    } else if (direction.x < 0) {  // Change to "< 0" for the correct check
        _transform.rotation = Quaternion.Euler(0, 180, 0);  // Rotate around the Y-axis by 180 degrees
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