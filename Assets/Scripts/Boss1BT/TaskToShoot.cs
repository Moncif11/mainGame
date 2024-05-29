using UnityEngine; 
using BehaviorTree; 

public class TaskToShoot : Node{
    Transform _transform; 
    GameObject _bulletPrefab;

    Health playerHealth; 

    public float attackTime = 1f; 
    public float attackCounter = 0; 
    public TaskToShoot(Transform transform ,GameObject bullet){
        _transform = transform;
        playerHealth = _transform.GetComponent<Health>();  
        _bulletPrefab = bullet;
    }

    public override NodeState Evaluate()
    {   
        Transform target = (Transform)GetData("target"); 
        Debug.Log("Attack");
        Debug.Log("Attackstate :" +state);
        attackCounter+= Time.deltaTime;
        if(attackCounter >= attackTime){           
            Debug.Log("Shoot: "+_bulletPrefab.name);
            if(!Boss1BT.isLeft){
                GameObject bullet = GameObject.Instantiate(_bulletPrefab, _transform.position + _transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(_transform.right*1000);
            }
            else{
                Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(_bulletPrefab, _transform.position + -_transform.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(-_transform.right*1000);
                Debug.Log("Bullet : " + bullet.transform.position);
            }
            if(playerHealth.health <= 0){
                Debug.Log("Data Cleared");
                ClearData("target"); 
                }
            else{
                attackCounter= 0f; 
            }
        }

        state = NodeState.RUNNING;
        return state;
    }
    
}