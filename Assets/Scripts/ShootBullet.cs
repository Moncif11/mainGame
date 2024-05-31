using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootBullet : StateMachineBehaviour
{
    public GameObject bulletPrefab;
    public Transform shootPoint; 
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        Debug.Log("Boss Shoot");
        if (shootPoint == null)
        {
            // Attempt to find shootPoint if not assigned in the inspector
            GameObject shootPointObject = GameObject.Find("ShootPoint");
            if (shootPointObject != null)
            {
                shootPoint = shootPointObject.transform;
            }
            else
            {
                Debug.LogError("ShootPoint not found");
                return;
            }
        }
        if(!Boss1BT.isLeft){
                GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPoint.position + shootPoint.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(shootPoint.right*1000);
            }
            else{
                Debug.Log("Shoot left");
                GameObject bullet = GameObject.Instantiate(bulletPrefab, shootPoint.position + -shootPoint.right, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>(); 
                bulletRB.AddForce(-shootPoint.right*1000);
                Debug.Log("Bullet : " + bullet.transform.position);
            }   
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
