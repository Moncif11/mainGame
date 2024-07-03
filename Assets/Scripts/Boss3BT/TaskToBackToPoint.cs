using BehaviorTree;
using UnityEngine;

namespace Boss3AI{
public class TaskToBackToPoint : Node
{
    Transform _transform; 
    Animator _animator; 

    Vector3 startposition; 

    float maxHealth; 

    float startSpeed = 3f; 
    Animator animator; 
    public TaskToBackToPoint(Transform transform){
        animator = transform.GetComponent<Animator>();  
        _transform = transform;
        _animator = transform.GetComponent<Animator>();
        startposition = transform.position;
        maxHealth = transform.GetComponent<Health>().maxHealth;
    }

    public override NodeState Evaluate()
    {  
            Boss2BT.isLeft=isLeft();
            float distance  = Vector2.Distance((Vector2)startposition,_transform.position); 
            if(distance > 0.001 && Boss3BT.backToStart == true){
            animator.SetTrigger("Walking"); 
            animator.ResetTrigger("Nothing"); 
            Vector2 direction = Vector2.MoveTowards(_transform.position,startposition, Boss2BT.speed*Time.deltaTime);
            _transform.position = new Vector2(direction.x,_transform.position.y);
            _transform.GetComponent<Health>().health = maxHealth; 
            Boss3BT.speed = startSpeed; 
             _transform.GetComponent<Health>().damageReduction = 100;  
             Boss3BT.bossPhase = BossPhase.ONE;   
            }
            else{
            animator.ResetTrigger("Walking"); 
            animator.SetTrigger("Nothing"); 
                _transform.GetComponent<Health>().damageReduction = 0;
                Boss2BT.backToStart = false; 
                Boss2BT.coolDownDodgeCounter = 0;
                Boss2BT.coolDownSP1Counter = 0;	
                Boss2BT.coolDownSP1Counter = 0;	
                Boss2BT.SP1Ready= true;
                Boss2BT.SP2Ready= true;
                Boss2BT.dogdeReady = true; 
            }
             animator.ResetTrigger("Melee");
            animator.ResetTrigger("Burn");
            animator.ResetTrigger("Teleport");
            animator.ResetTrigger("Ice rain");
        state = NodeState.RUNNING; 
        return state; 
    }
   private bool isLeft() {
    Vector2 direction = (_transform.position - startposition).normalized;
    if (direction.x > 0) {
        Boss2BT.isLeft = true;
        _transform.rotation = Quaternion.Euler(0, 0, 0);  // Rotate around the Y-axis by 180 degrees
        return true;
    } else if (direction.x < 0) {  // Change to "< 0" for the correct check
        Boss2BT.isLeft = true;
        _transform.rotation = Quaternion.Euler(0, 180, 0); // Rotate around the Y-axis by 180 degrees
        return false;
    }
    return true;
    }
    }
}