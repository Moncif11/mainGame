using UnityEngine; 
using BehaviorTree;

namespace MidRangeMonsterAI{
public class TaskAttack : Node{
    Health capePlayerHealth; 
    Transform _lastTarger; 

    Transform _transform; 
    Rigidbody2D rb ; 
    public float attackTime = 2f; 
    public float attackCounter = 0; 

    Animator animator;

    public TaskAttack( Transform transform){
       this._transform = transform;
       animator = transform.GetComponent<Animator>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target"); 
        if(_lastTarger != target){
            _lastTarger = target;
            capePlayerHealth = target.GetComponent<Health>();
        }  
        attackCounter+= Time.deltaTime;
        if(attackCounter >=attackTime){
            Melee attack = _transform.GetComponent<Melee>();
            MidRangeMonsterBT.isRight = isRight(target);
            animator.SetTrigger("Attack");
            animator.ResetTrigger("Running");
            animator.ResetTrigger("Guard");
            Debug.Log("attack");
            if(MidRangeMonsterBT.isRight) 
            rb.velocity = Vector2.right*25;
            else rb.velocity = Vector2.left*25;
             if(capePlayerHealth.health <= 0){
                Debug.Log("Data Cleared");
                ClearData("target"); 
                }
            else{
                attackCounter= 0f; 
            }  
        }
        state= NodeState.RUNNING; 
        return state; 
    }

     private bool isRight(Transform target){
         Vector2 direction = (_transform.position - target.position).normalized; 
         if (direction.x > 0)
        {
             Vector3 Scaler = _transform.localScale;
             if(Scaler.x >0){
                 Scaler.x = -Scaler.x;
             } 
            _transform.localScale = Scaler;
            return false; 
        }
        else if (direction.x <0)
        {
            RangeMonsterBT.isRight = true; // Nach rechts schauen
              Vector3 Scaler = _transform.localScale;
            Scaler.x = Mathf.Abs(Scaler.x); 
            _transform.localScale = Scaler;
            return true;
        }  
        return true; 
    }
    }
    }