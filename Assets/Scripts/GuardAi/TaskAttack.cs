using UnityEngine; 
using BehaviorTree; 

public class TaskAttack : Node{
    CapePlayerController capePlayerController; 
    Transform _lastTarger; 

    public float attackTime = 1f; 
    public float attackCounter = 0;
    public TaskAttack(Transform transform){
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target"); 
        if(_lastTarger != target){
            _lastTarger = target;
            capePlayerController = target.GetComponent<CapePlayerController>();
        }
        attackCounter += Time.deltaTime;
        if(attackCounter >= attackTime){
            capePlayerController.takeDamage(2); 
            if(capePlayerController.health<= 0){
                ClearData("target"); 
            }
            else{
                attackCounter= 0f; 
            }
        }
        state= NodeState.RUNNING; 
        return state; 

    }

}