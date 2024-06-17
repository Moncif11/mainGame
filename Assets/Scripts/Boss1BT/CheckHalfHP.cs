using BehaviorTree;
using UnityEngine;

namespace Boss1AI{
public class CheckHalfHP : Node{

    Transform _transform;
    Health _health; 
     public CheckHalfHP(Transform transform){
        _transform = transform;
        _health = _transform.GetComponent<Health>(); 
    }
    public override NodeState Evaluate()
    {   // Aktualisieren Sie den HP-Wert jedes Mal, wenn die Methode aufgerufen wird
        float currentHP = _health.health;
        float maxHP = _health.maxHealth;

        if(currentHP<=(maxHP/2)&& Boss1BT.bossPhase== BossPhase.ONE){
            state= NodeState.SUCCESS; 
            return state;
        }
        state = NodeState.FAILURE;
        return state;
        }
    }
}