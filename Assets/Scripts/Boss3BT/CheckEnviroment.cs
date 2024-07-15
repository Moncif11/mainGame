using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine;

namespace Boss3AI{
public class CheckEnviroment : Node{
    Transform _transform;
    Vector3 _position; 

    GameObject _healthbar; 

    public CheckEnviroment(Transform transform ,GameObject healthbar){
        _transform = transform;
        _position = transform.position;
        _healthbar = healthbar;
    }

    public override NodeState Evaluate()
    {  
         Transform target = nearestPlayer();
        float distance = Vector2.Distance(_transform.position,_position);   
        float playerEnviromentRadius = Vector2.Distance(target.position,_position); 
        if(Boss3BT.backToStart == true){
        state = NodeState.FAILURE;
        return state; 
        }
        if(distance <25 && playerEnviromentRadius < 25 ){
            _healthbar.SetActive(true);
            state = NodeState.SUCCESS; 
            return state; 
        } 
        Boss2BT.backToStart = true; 
        Debug.Log("Boss 3 BackToStart");
        state = NodeState.FAILURE; 
        return state;
    }

private bool isLeft(Transform target) {
    Vector2 direction = (_transform.position - target.position).normalized;
    if (direction.x > 0) {
        Boss2BT.isLeft = true;
        _transform.Rotate(0, 0, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    } else if (direction.x < 0) {  // Change to "< 0" for the correct check
        Boss2BT.isLeft = true;
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