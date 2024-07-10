using UnityEngine; 
using BehaviorTree;

namespace RangeMonsterAI {
public class TaskAttack : Node{
    Health capePlayerHealth; 
    Transform _lastTarger; 

    Transform _transform; 
    GameObject bulletPrefab; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 

    Animator animator;

    AudioManager audioManager;

    public TaskAttack(GameObject bulletPrefab , Transform transform){
       this.bulletPrefab = bulletPrefab;
       this._transform = transform;
       animator = _transform.GetComponent<Animator>();
       audioManager = GameObject.FindObjectOfType<AudioManager>();
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
            Debug.Log("Shoot: "+bulletPrefab.name);
            animator.SetTrigger("Attack");
            animator.ResetTrigger("Running"); 
            if(audioManager!=null){
                audioManager.Play("Shoot");
            }  
            if(RangeMonsterBT.isRight){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, _transform.position + _transform.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, 90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(_transform.right*1000);
            }
            else{
                Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(bulletPrefab, _transform.position + -_transform.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, -90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(-_transform.right*1000);
                Debug.Log("Bullet : " + bullet.transform.position);
            }
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

}
}