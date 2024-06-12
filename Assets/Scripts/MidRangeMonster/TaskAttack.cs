using UnityEngine; 
using BehaviorTree;

namespace MidRangeMonsterAI{
public class TaskAttack : Node{
    Health capePlayerHealth; 
    Transform _lastTarger; 

    Transform _transform; 
    Rigidbody2D rb ; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 

    Animator animator;

    public TaskAttack( Transform transform){
       this._transform = transform;
       animator = _transform.GetComponent<Animator>();
      rb = transform.GetComponent<Rigidbody2D>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target"); 
        Debug.Log("Attack");
        Debug.Log("Attackstate :" +state);
        if(_lastTarger != target){
            _lastTarger = target;
            capePlayerHealth = target.GetComponent<Health>();
        }  
        attackCounter+= Time.deltaTime;
        if(attackCounter > attackTime){
            if(MidRangeMonsterBT.isRight) 
            rb.velocity = Vector2.right*15;
            else rb.velocity = Vector2.left*15;  
        }
            if(capePlayerHealth.health <= 0){
                Debug.Log("Data Cleared");
                ClearData("target"); 
                }
            else{
                attackCounter= 0f; 
            }
        
        state= NodeState.RUNNING; 
        return state; 
    }
    }
    }