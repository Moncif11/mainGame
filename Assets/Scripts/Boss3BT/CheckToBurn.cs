using BehaviorTree; 
using UnityEngine; 

namespace Boss3AI{
    public class CheckToBurn : Node{

        Transform _transform;
        public CheckToBurn(Transform transform){
            _transform = transform;
        }

        public override NodeState Evaluate(){
            if(Boss3BT.SP1Ready){
                Transform playerTransform = nearestPlayer();  
                parent.parent.parent.parent.SetData("target",(Object)playerTransform);
                state = NodeState.SUCCESS;
                return state;
            }
            state = NodeState.FAILURE; 
            return state;           
        }

    private Transform nearestPlayer(){
        GameObject[] gameObjects= GameObject.FindGameObjectsWithTag("Player"); 
        float minDistance = float.MaxValue;
        GameObject player =null; 
        foreach( GameObject gameObject in gameObjects ){
                if(Vector2.Distance(_transform.position,gameObject.transform.position) < minDistance){
                    minDistance = Vector2.Distance(_transform.position,gameObject.transform.position); 
                    player=gameObject; 
                }
        }
        return player.transform;
    }
    }
} 