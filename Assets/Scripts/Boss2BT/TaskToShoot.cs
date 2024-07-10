using UnityEngine; 
using BehaviorTree;
using UnityEngine.UIElements;

namespace Boss2AI{
public class TaskToShoot : Node{
    Health capePlayerHealth; 
    Transform _lastTarger; 

    Transform _transform; 
    GameObject bulletPrefab; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 

    Transform[] _shootpoints;
    int _shootpointIndex = 0; 

    Animator animator;

    public TaskToShoot(Transform transform,GameObject bulletPrefab, Transform[] shootpoints){
       this.bulletPrefab = bulletPrefab;
       this._transform = transform;
       animator = _transform.GetComponent<Animator>();
       _shootpoints = shootpoints; 
    }

    public override NodeState Evaluate()
    {
        Transform target = (Transform)GetData("target");
        Transform shootPosition = _shootpoints[_shootpointIndex];
        if(_lastTarger != target){
            _lastTarger = target;
            capePlayerHealth = target.GetComponent<Health>();
        }  
        attackCounter+= Time.deltaTime;
        if(attackCounter >= attackTime){           
            Debug.Log("Shoot: "+bulletPrefab.name);
            Boss2BT.isLeft = isLeft(target);
            animator.SetTrigger("Shoot");
            animator.ResetTrigger("Walking"); 
            animator.ResetTrigger("Idle"); 
            animator.ResetTrigger("Jump"); 
            GameObject.FindObjectOfType<AudioManager>().Play("Shoot"); 
                Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPosition.position + -shootPosition.right, Quaternion.identity);
                Quaternion rotation = bullet.transform.rotation;
                //rotation *= Quaternion.Euler(0, 0, -90); // Ändere die Rotation um -90 Grad um die Z-Achse
                bullet.transform.rotation = rotation;
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(-_transform.right*1000);
                Debug.Log("Bullet : " + bullet.transform.position);
                attackCounter= 0f; 
                _shootpointIndex= (_shootpointIndex+1)%_shootpoints.Length;
                
        }
        state= NodeState.RUNNING; 
        return state; 
    }

   private bool isLeft(Transform target) {
   Vector2 direction = (_transform.position - target.position).normalized;
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