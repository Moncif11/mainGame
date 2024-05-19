using UnityEngine; 
using BehaviorTree; 

public class TaskAttack : Node{
    CapePlayerController capePlayerController; 
    Transform _lastTarger; 

    GameObject bulletPrefab; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 
    public TaskAttack(GameObject bulletPrefab){
       this.bulletPrefab = bulletPrefab;
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target"); 
        Debug.Log("Attack");
        Debug.Log("Attackstate :" +state);
        if(_lastTarger != target){
            _lastTarger = target;
            capePlayerController = target.GetComponent<CapePlayerController>();
        }   
        if(capePlayerController.health<= 0){
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