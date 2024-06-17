using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree; 

namespace Boss1AI{
public class CheckMeleeAttack : Node
{
    Transform _transform;
    public CheckMeleeAttack(Transform transform){
        _transform = transform; 
    } 
    public override NodeState Evaluate(){
    Transform t = nearestPlayer();
    if(Vector2.Distance(_transform.position,t.position)<Boss1BT.meleeRange){
        parent.parent.SetData("target",(Object)t);
        state = NodeState.SUCCESS; 
        return state;    
    }
    state = NodeState.FAILURE; 
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