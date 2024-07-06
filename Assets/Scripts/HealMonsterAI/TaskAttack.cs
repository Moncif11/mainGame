using UnityEngine; 
using BehaviorTree;

namespace HealMonsterAI{
public class TaskAttack : Node{
    Health capePlayerHealth; 
    Transform _lastTarger; 

    Transform _transform; 
    GameObject bulletPrefab; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 

    Animator animator;

    public TaskAttack(GameObject bulletPrefab , Transform transform){
       this.bulletPrefab = bulletPrefab;
       this._transform = transform;
       animator = _transform.GetComponent<Animator>();
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        //Debug.Log("Attack");
        if(_lastTarger != target){
            _lastTarger = target;
            capePlayerHealth = target.GetComponent<Health>();
        }  
        attackCounter+= Time.deltaTime;
        if(attackCounter >= attackTime){           
            //Debug.Log("Shoot: "+bulletPrefab.name);
            HealMonsterBT.isRight = isRight(target);
            animator.SetTrigger("Attack");
            animator.ResetTrigger("Running"); 
            if(HealMonsterBT.isRight){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, _transform.position + _transform.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, 90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(_transform.right*1000);
            }
            else{
                //Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(bulletPrefab, _transform.position + -_transform.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                rotation *= Quaternion.Euler(0, 0, -90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(-_transform.right*1000);
            }
                attackCounter= 0f; 
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