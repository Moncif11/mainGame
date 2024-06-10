using BehaviorTree;

using UnityEngine;

namespace HealMonsterAI{
public class CheckToHeal : Node
{   Transform _transform; 

    bool isFacing = false;  
    public CheckToHeal(Transform transform){
    _transform = transform;
    }
    public override NodeState Evaluate()
    {
         GameObject[] enemies= GameObject.FindGameObjectsWithTag("Enemy"); 
        //if(t==null){
            Debug.Log(enemies.Length);
            if(enemies.Length == 0){
                state=NodeState.FAILURE; 
                return state; 
            }
            
            Transform closestEnemy = null;
            float minDistance = float.MaxValue;

            foreach (GameObject enemy in enemies) {
                float distance = Vector2.Distance(_transform.position, enemy.transform.position);
                if (distance < minDistance) {
                    if (enemy.transform == _transform) {
                    continue;
                }
                    minDistance = distance;
                    closestEnemy = enemy.transform;
                }
            }

            if (closestEnemy != null) {
                Health health = closestEnemy.GetComponent<Health>();
                if (health != null && health.health <= health.maxHealth / 2) {
                    parent.parent.SetData("target", closestEnemy);
                    Debug.Log("CheckToHeal: SUCCESS");
                    state = NodeState.SUCCESS;
                    return state;
                }
            }
                state= NodeState.FAILURE; 
                return state;
    }
}
}