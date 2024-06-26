using BehaviorTree;
using UnityEngine; 

namespace Boss2AI{
    public class CheckToDodge: Node{
        Transform _transform; 
        public CheckToDodge(Transform transform){
            _transform = transform;
        }
        public override NodeState Evaluate()
        {   
            if(!Boss2BT.dogdeReady){
                state = NodeState.FAILURE;
                return state; 
            }   
            if(Boss2BT.isJumping || Boss2BT.isFalling){
                state = NodeState.SUCCESS;
                return state; 
            }
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 5f); 
            GameObject bullet = null; 
            if(colliders.Length == 0){
                 state = NodeState.FAILURE;
                return state;    
            }
            foreach (Collider2D collider in colliders){
                if (collider == null || collider.gameObject == null)
                {
                    Debug.LogWarning("Collider or collider.gameObject is null");
                    continue;
                }

                if(collider.gameObject.CompareTag("Bullet")){
                    if(collider.GetComponent<BulletUser>().user == User.PLAYER ){
                    Debug.Log("Found Attack to Dogde");
                    bullet = collider.gameObject; 
                    break;
                    }
                }

                CapePlayerController capePlayerController = collider.gameObject.GetComponent<CapePlayerController>();
                if (capePlayerController != null && capePlayerController.dash)
                {
                    Debug.Log("Found Player Dash to Dodge");
                    bullet = collider.gameObject;
                    break;
                }
            }
            if(bullet!=null){
            Debug.Log("CheckToDoge");
            state = NodeState.SUCCESS; 
            return state; 
            }
            Debug.Log("CheckToDoge : Failed");
            state = NodeState.FAILURE; 
            return state; 
        }
    }
}