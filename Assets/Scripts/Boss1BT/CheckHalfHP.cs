using BehaviorTree;
using UnityEngine;

public class CheckHalfHP : Node{

    Transform _transform;
    float _hp;

    float _maxHP;
     public CheckHalfHP(Transform transform){
        _transform = transform;
        _hp = transform.GetComponent<Health>().health;
        _maxHP = transform.GetComponent<Health>().maxHealth;
    }
    public override NodeState Evaluate()
    {
        if(_hp<=(0.5 *_maxHP)&& Boss1BT.bossPhase== BossPhase.ONE){
            Boss1BT.bossPhase = BossPhase.TWO;
            state= NodeState.SUCCESS; 
            return state;
        }
        state = NodeState.FAILURE;
        return state;
    }

}