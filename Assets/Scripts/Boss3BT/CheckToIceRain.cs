using BehaviorTree; 
using UnityEngine; 

namespace Boss3AI{
    public class CheckToIceRain : Node{

        Transform _transform;
        public CheckToIceRain(Transform transform){
            _transform = transform;
        }

        public override NodeState Evaluate(){
            if(GetData("Ice Rain") != null ){
                state = NodeState.SUCCESS; 
                return state; 
            }
            if(Boss3BT.SP2Ready){
                Debug.Log("Start");
                Transform playerTransform = nearestPlayer();  
                parent.parent.parent.parent.SetData("Ice Rain",(Object)playerTransform);
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