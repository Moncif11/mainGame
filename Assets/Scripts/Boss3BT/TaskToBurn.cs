using UnityEngine; 
using BehaviorTree;
using UnityEngine.UIElements;

namespace Boss3AI{
public class TaskToBurn : Node{
    Health capePlayerHealth;
    Transform _transform; 
    GameObject bulletPrefab; 
    public float attackTime = 1f; 
    public float attackCounter = 0; 
    Animator animator;

    public TaskToBurn(Transform transform,GameObject bulletPrefab){
       this.bulletPrefab = bulletPrefab;
       this._transform = transform;
       animator = _transform.GetComponent<Animator>();
     
    }

    public override NodeState Evaluate()
    {   
        Transform target = (Transform)GetData("target");
        attackCounter+= Time.deltaTime;
        Debug.Log("Burn");
        if(attackCounter >= attackTime){      
            animator.ResetTrigger("Melee");
            animator.ResetTrigger("Walking"); 
            animator.ResetTrigger("Nothing"); 
            animator.SetTrigger("Burn"); 
            animator.ResetTrigger("Teleport"); 
            animator.ResetTrigger("Ice Rain");      
            Boss2BT.isLeft = isLeft(target);
                Debug.Log("Shoot left");
                GameObject.FindObjectOfType<AudioManager>().Play("Boss3 Burn"); 
                GameObject bullet = GameObject.Instantiate(bulletPrefab, target.position, Quaternion.identity);
                Boss3BT.SP1Ready = false; 
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