using BehaviorTree;
using UnityEngine;
using UnityEngine.UIElements;

namespace Boss3AI{
    public class CheckToTeleport: Node{
        Transform _transform; 
        Vector3 position; 
        public CheckToTeleport(Transform transform){
            _transform = transform;
            position = transform.position;
        }
        public override NodeState Evaluate()
        {   
            if(!Boss3BT.teleportReady){
                state = NodeState.FAILURE;
                return state; 
            }   
            Collider2D[] objects = Physics2D.OverlapCircleAll(_transform.position,5.0f);
            GameObject player = null ; 
            foreach (Collider2D collider in objects){
                if (collider == null || collider.gameObject == null)
                {
                    Debug.LogWarning("Collider or collider.gameObject is null");
                    continue;
                }

                if(collider.gameObject.CompareTag("Player")){
                    if(Vector2.Distance(collider.gameObject.transform.position,position) <25){
                          player = collider.gameObject;        
                    }
                    }
            }

            if(player != null){
            parent.parent.parent.parent.SetData("target",(Object)player.transform);
            state = NodeState.SUCCESS; 
            return state; 
            }
            state = NodeState.FAILURE; 
            return state; 
        }
    }
}