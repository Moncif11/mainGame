using BehaviorTree;
using UnityEngine;
namespace Boss2AI{
    public class CheckToFireBeam : Node{

        Transform _transform;

        public CheckToFireBeam(Transform transform ){
            _transform = transform;
        }

        public override NodeState Evaluate(){
            bool inRange = playerInLaserRange(); 
            if(!Boss2BT.SP2Ready || !inRange){
                state = NodeState.FAILURE;
                return state; 
            }
            Debug.Log("CheckLaser Success");
            state = NodeState.SUCCESS;
            return state; 
        }

        private bool playerInLaserRange(){
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject player in players){
             float distance = Mathf.Abs(_transform.position.x-player.transform.position.x); 
            if(!inSight(player.transform)){
                continue;
            }
             if(Boss2BT.maxDistanceLaser>distance&&distance>Boss2BT.minDistanceLaser){
                return true;
             }   
            }
            return false;   
        }

    private bool inSight(Transform playerTransform){
        Vector2 direction = _transform.position - playerTransform.position; 
      if(direction.x<0 && !Boss2BT.isLeft){
            return true; 
        }
        else if (direction.x>0 && Boss2BT.isLeft){
            return true;
        }
        else{
            return false;
        }
    }
    }
}