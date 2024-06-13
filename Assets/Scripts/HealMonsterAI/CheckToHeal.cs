using BehaviorTree;

using UnityEngine;

namespace HealMonsterAI{
public class CheckToHeal : Node
{   Transform _transform; 

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
                if (distance < minDistance && distance < HealMonsterBT.attackRange) {
                    if (enemy.transform == _transform) {
                    continue;
                }
                Health health = enemy.GetComponent<Health>();
                if (health != null && health.health <= (health.maxHealth/2)) {
                    minDistance = distance;
                    closestEnemy = enemy.transform;
                    }
                }
            }
            if (closestEnemy != null) {
                    parent.parent.SetData("target", closestEnemy);
                    state = NodeState.SUCCESS;
                    return state;
            }
                state= NodeState.FAILURE; 
                return state;
        }
    }
}