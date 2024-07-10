using UnityEngine;
using BehaviorTree;

namespace Boss1AI{
public class TaskToShoot : Node
{
    Transform _transform;
    Health playerHealth;

    public float attackTime = 0.7f;
    public float attackCounter = 0;

    private float lastAttackTime = -Mathf.Infinity; // Initialize to a very old time

    Animator _animator;

    public TaskToShoot(Transform transform)
    {
        _transform = transform;
        playerHealth = _transform.GetComponent<Health>();
        _animator = transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");

        if (target == null)
        {
            state = NodeState.FAILURE;
            return state;
        }     
        Boss1BT.isLeft = IsLeft(target);
        attackCounter+=Time.deltaTime;
        if(attackCounter>=attackTime){    
        _animator.ResetTrigger("SP2");
        _animator.ResetTrigger("Walking");
        _animator.ResetTrigger("SP1");
        _animator.ResetTrigger("Melee");
        _animator.SetTrigger("Shoot");
        Debug.Log("Shoot");
        Boss1BT.amountShoot--;
        if (playerHealth.health <= 0)
        {
            Debug.Log("Data Cleared");
            ClearData("target");
        }
        else{
            attackCounter =0;
        }
        }
        state = NodeState.RUNNING;
        return state;
        }


    private bool IsLeft(Transform target)
    {
        Vector2 direction = (_transform.position - target.position).normalized;
        Vector3 scaler = _transform.localScale;

        if (direction.x > 0)
        {
            if (scaler.x < 0)
            {
                scaler.x = -scaler.x;
            }
            _transform.localScale = scaler;
            return true;
        }
        else if (direction.x < 0)
        {
            if (scaler.x > 0)
            {
                scaler.x = -scaler.x;
            }
            _transform.localScale = scaler;
            return false;
        }

        return true;
        }
    }
}
