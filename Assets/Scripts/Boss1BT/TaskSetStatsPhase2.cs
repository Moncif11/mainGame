using BehaviorTree;
using UnityEngine;

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
    {
        state = NodeState.RUNNING;
        return state;
    }

}