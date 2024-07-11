using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine;

namespace Boss1AI{
public class CheckEnviroment : Node{
    Transform _transform;
    Vector3 _position; 

    public CheckEnviroment(Transform transform){
        _transform = transform;
        _position = transform.position;
    }

    public override NodeState Evaluate()
    {  
         Transform target = nearestPlayer();
        float distance = Vector2.Distance(_transform.position,_position);   
        float playerEnviromentRadius = Vector2.Distance(target.position,_position); 
        if(Boss1BT.backToStart == true){
        state = NodeState.FAILURE;
        return state; 
        }
        if(distance <25 && playerEnviromentRadius < 25 ){
            state = NodeState.SUCCESS; 
            return state; 
        } 
        Boss1BT.backToStart = true; 
        Debug.Log("BackToStart");
        state = NodeState.FAILURE; 
        return state;
    }

private bool isLeft(Transform target) {
    Vector2 direction = (_transform.position - target.position).normalized;
    if (direction.x > 0) {
        Boss1BT.isLeft = true;
        _transform.Rotate(0, 0, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    } else if (direction.x < 0) {  // Change to "< 0" for the correct check
        Boss1BT.isLeft = true;
        _transform.Rotate(0, 180, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    }
    return true;
}
    /*
    }check the player in the Enviroment of the boss
    */
    private Transform nearestPlayer(){
        float minDistance =0f; 
        Transform targetPlayer; 
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); 
            minDistance = Vector2.Distance(_position,players[0].transform.position); 
            targetPlayer = players[0].transform;
            for(int i = 1; i < players.Length; i++){
            if(Vector2.Distance(_position,players[0].transform.position)<minDistance){
                minDistance = Vector2.Distance(_transform.position,players[0].transform.position); 
                targetPlayer = players[i].transform;
            }
            }
            return targetPlayer;
        }
    }
}