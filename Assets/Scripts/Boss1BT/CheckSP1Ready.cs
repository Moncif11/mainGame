using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class CheckSP1Ready : Node
{   
    Transform _transform; 

    public CheckSP1Ready(Transform transform){
        _transform = transform;
    }
    public override NodeState Evaluate()
    {   Transform target = nearestPlayer();
        if(Boss1BT.SP1Ready && BossPhase.TWO == Boss1BT.bossPhase){
             parent.parent.parent.SetData("target",(Object)target);
            state = NodeState.SUCCESS; 
        }
        else{
            state = NodeState.FAILURE; 
        }
        Debug.Log("SP1 Check: " + state);
        return state; 
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
