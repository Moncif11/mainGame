using System;
using BehaviorTree;
using Unity.VisualScripting;
using UnityEngine; 

namespace MidRangeMonsterAI{
    public class CheckGuard : Node{
        Transform _transform; 
        bool isFacing = true; 
        public CheckGuard(Transform transform){
            _transform = transform;
        }
        public override NodeState Evaluate()
        {   
            Collider2D[] colliders = Physics2D.OverlapCircleAll(_transform.position, 5f); 
            GameObject bullet = null; 
            float minDistance = float.MaxValue;
            foreach (Collider2D collider in colliders){
                if(collider.gameObject.CompareTag("Bullet")){
                     float distance = Vector2.Distance(_transform.position, collider.transform.position);
                    facingTarget(collider.transform); 
                    if(distance < minDistance && isFacing){
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
        
         private void facingTarget(Transform target){
            Vector2 direction = (_transform.position - target.position).normalized;
            if(direction.x >0 &&  MidRangeMonsterBT.isRight){
                isFacing = true;
            }
            else if(direction.x >0 &&  !MidRangeMonsterBT.isRight){
                isFacing = true; 
            }
            else isFacing = false; 
        }
    }
}