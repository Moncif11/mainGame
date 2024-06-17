using BehaviorTree;
using UnityEngine;

namespace Boss1AI{
public class TaskSetStatsPhase2 : Node{

    Transform _transform;
    float _hp;

    float _maxHP;
     public TaskSetStatsPhase2(Transform transform){
        _transform = transform;
        _hp = transform.GetComponent<Health>().health;
        _maxHP = transform.GetComponent<Health>().maxHealth;
    }
    public override NodeState Evaluate()
    {   Debug.Log("Boss Phase 2");
        Boss1BT.bossPhase = BossPhase.TWO;
        Boss1BT.attack *= 1.5f; 
        Boss1BT.speed *= 3;
        state = NodeState.RUNNING;
        return state;
    }

    }
}