using UnityEngine; 
using BehaviorTree; 

public class TaskAttack : Node{
    Health capePlayerHealth; 
    Transform _lastTarger; 

    Transform _transform; 
    GameObject bulletPrefab; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 

    public TaskAttack(GameObject bulletPrefab , Transform transform){
       this.bulletPrefab = bulletPrefab;
       this._transform = transform;
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
        if(attackCounter >= attackTime){
            _transform.position = _transform.position;
            if(GuardBT.isRight){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, _transform.position + -_transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(-_transform.right*1000);
            }
            else{
                GameObject bullet = GameObject.Instantiate(bulletPrefab, _transform.position + _transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(_transform.right*1000);
            }
        if(capePlayerHealth.health<= 0){
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

}