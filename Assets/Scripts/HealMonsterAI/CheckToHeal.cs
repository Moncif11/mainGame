using BehaviorTree;

using UnityEngine;

public class CheckToHeal : Node
{   Transform _transform; 

    bool isFacing = false;  
    public CheckToHeal(Transform transform){
    _transform = transform;
    }
    public override NodeState Evaluate()
    {
        float minDistance = 0f;
         Transform targetEnemy= GameObject.FindGameObjectWithTag("Enemy").transform; 
        //if(t==null){
            if(targetEnemy ==null){
                state=NodeState.FAILURE; 
                return state; 
            }
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 
            minDistance = Vector2.Distance(_transform.position,enemies[0].transform.position); 
            targetEnemy = enemies[0].transform;
            for(int i = 1; i < enemies.Length; i++){
            if(Vector2.Distance(_transform.position,enemies[i].transform.position)<minDistance){
                if(_transform = enemies[i].transform){continue;}
                minDistance = Vector2.Distance(_transform.position,enemies[i].transform.position); 
                targetEnemy = enemies[i].transform;
            }
            }
                Health health = targetEnemy.GetComponent<Health>() ;          
        //}
        if(minDistance <= HealMonsterBT.attackRange && health.maxHealth/2 >= health.health){
                parent.parent.SetData("target",targetEnemy); 
                Debug.Log("CheckEnemyInFOVRange : SUCCESS");
                state= NodeState.SUCCESS; 
                return state;
        }
                state= NodeState.FAILURE; 
                return state;
    }
}