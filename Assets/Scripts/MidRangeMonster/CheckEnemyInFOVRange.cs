using System.Runtime.Versioning;
using BehaviorTree;
using UnityEngine;
namespace MidRangeMonsterAI{
public class CheckEnemyInFOVRange : Node
{
    public Transform _transform; 
 
    public CheckEnemyInFOVRange(Transform transform){
        _transform = transform;

    }   

    public override NodeState Evaluate(){
        object t = GetData("target");
         float minDistance = 0f;
         Transform targetPlayer= GameObject.FindGameObjectWithTag("Player").transform; 
        //if(t==null){
            if(targetPlayer ==null){
                state=NodeState.FAILURE; 
                return state; 
            }
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player"); 
            minDistance = Vector2.Distance(_transform.position,players[0].transform.position); 
            targetPlayer = players[0].transform;
            for(int i = 1; i < players.Length; i++){
            if(Vector2.Distance(_transform.position,players[i].transform.position)<minDistance){
                minDistance = Vector2.Distance(_transform.position,players[i].transform.position); 
                targetPlayer = players[i].transform;
            }
            }
        //}
        if(minDistance <= RangeMonsterBT.fovRange){
                parent.parent.SetData("target",targetPlayer); 
                Debug.Log("CheckEnemyInFOVRange : SUCCESS");
                state= NodeState.SUCCESS; 
                return state;
        }
            Debug.Log("CheckEnemyInFOVRange : FAILURE");
            state= NodeState.FAILURE; 
            return state;
        }
    }
}