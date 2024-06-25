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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 5f); 
            GameObject bullet = null; 
            float minDistance = float.MaxValue;
            foreach (Collider2D collider in colliders){
                if(collider.gameObject.CompareTag("Bullet") || collider.gameObject.GetComponent<CapePlayerController>().dash){
                     float distance = Vector2.Distance(_transform.position, collider.transform.position);
                    if(distance < minDistance ){
                        minDistance = distance;
                        bullet = collider.gameObject;
                    }
                }
            }
            if(bullet!=null){
            state = NodeState.RUNNING; 
            return state; 
            }
            state = NodeState.FAILURE; 
            return state; 
        }
        
    
    }
}