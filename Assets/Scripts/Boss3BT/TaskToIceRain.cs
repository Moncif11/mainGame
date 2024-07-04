using BehaviorTree; 
using UnityEngine; 

namespace Boss3AI{
    public class TaskToIceRain : Node{

        Transform _transform;
        Animator _animator; 
        GameObject _bulletPrefab; 
        float spawnCounter = 0; 
        float durationCounter = 0; 
        float duration = 7.0f; 
        float spawnDuration = 0.5f ;

        public TaskToIceRain(Transform transform ,GameObject bulletPrefab){
            _transform = transform;
            _animator = transform.GetComponent<Animator>();
            _bulletPrefab = bulletPrefab;
        }

  public override NodeState Evaluate() {
    Transform position = (Transform)GetData("Ice Rain");
    if (position == null) {
        // Wenn die Position nicht gefunden wurde, setzen Sie den Zustand auf FAILURE
        state = NodeState.FAILURE;
        return state;
    }

    durationCounter += Time.deltaTime;
    spawnCounter += Time.deltaTime;

    if (durationCounter < duration) {
        if(durationCounter == 0){
            _animator.ResetTrigger("Melee");
            _animator.ResetTrigger("Walking"); 
            _animator.ResetTrigger("Nothing"); 
            _animator.SetTrigger("Burn"); 
            _animator.ResetTrigger("Teleport"); 
            _animator.ResetTrigger("Ice Rain");      
        }
        if (spawnCounter >= spawnDuration) {
            Debug.Log("Ice Rain");
            float offSet = Random.Range(-2, 2);
            Vector2 spawnPosition = new Vector2(position.position.x + offSet, 20);
            GameObject bullet = GameObject.Instantiate(_bulletPrefab, spawnPosition, Quaternion.identity);
            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            // Überprüfen Sie, ob das Rigidbody2D korrekt ist
            if (bulletRB != null) {
                bulletRB.AddForce(Vector2.down * 1000);
                Debug.Log("Bullet : " + bullet.transform.position);
            } else {
                Debug.LogError("Rigidbody2D nicht gefunden an Bullet Prefab.");
            }

            spawnCounter = 0;
        }
    } else {
        // Reset der Counter und Flags
        spawnCounter = 0;
        durationCounter = 0;
        Boss3BT.SP2Ready = false;
        ClearData("Ice Rain");

        state = NodeState.SUCCESS; // Zustand auf SUCCESS setzen
        return state;
    }

    state = NodeState.RUNNING;
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