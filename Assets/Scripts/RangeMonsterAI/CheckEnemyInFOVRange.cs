using BehaviorTree;
using UnityEngine;

public class CheckEnemyInFOVRange : Node
{
    public Transform _transform; 
 
    public CheckEnemyInFOVRange(Transform transform){
        _transform = transform;

    }   

    public override NodeState Evaluate(){
        object t = GetData("target");
        if(t==null){
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            float minDistance = 0f; 
            minDistance = Vector2.Distance(_transform.position,players[0].transform.position); 
            Transform targetPlayer = players[0].transform;
            for(int i = 1; i < players.Length; i++){
            if(Vector2.Distance(_transform.position,players[0].transform.position)<minDistance){
                minDistance = Vector2.Distance(_transform.position,players[0].transform.position); 
                targetPlayer = players[i].transform;
            }
            }
            if(minDistance <= GuardBT.fovRange){
                parent.parent.SetData("target",targetPlayer); 
                Debug.Log("CheckEnemyInFOVRange : SUCCESS");
                state= NodeState.SUCCESS; 
                return state;
            }
            state= NodeState.FAILURE; 
            return state;
        }
        state = NodeState.RUNNING; 
        return state; 
    }
}
