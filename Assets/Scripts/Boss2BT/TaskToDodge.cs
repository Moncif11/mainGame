using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using Unity.VisualScripting;

namespace Boss2AI{
public class TaskToDodge : Node
{private Transform _transform;
        private Transform _groundCheck; 
        private Rigidbody2D _rigidbody2D;
        private Vector2 originalPosition;
        private float jumpHeight = 5f; // Höhe des Sprungs
        private float jumpDuration = 0.5f; // Dauer des Sprungs in Sekunden
        private float groundCheckRadius = 0.1f; // Radius des Bodenkreises
        private LayerMask groundLayer; // LayerMask für den Boden 
        float elapsedTime = 0; 
    public TaskToDodge(Transform transform, Transform groundCheck,LayerMask groundLayer){
        _transform = transform;
        _groundCheck = groundCheck;
        this.groundLayer = groundLayer;
    }
        public override NodeState Evaluate(){
             bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, groundLayer);
              if (isGrounded && !Boss2BT.isJumping && !Boss2BT.isFalling)
            {
                StartJump();
                Debug.Log("Startjumping");
            }
            if (Boss2BT.isJumping)
            {
                PerformJump();
                 Debug.Log("Perform jumping");
            }
            if(Boss2BT.isFalling){
                PerformFall();
            } 
        
            state = NodeState.RUNNING;
            return state;
        }

        private void StartJump()
        {
            Boss2BT.isJumping = true;
            originalPosition = _transform.position;
        }

        private void PerformJump()
        {
            if (elapsedTime < jumpDuration)
            {       elapsedTime += Time.deltaTime;
                    Vector2 targetPosition = new Vector2(originalPosition.x, originalPosition.y + 3);
                // Bewege das Transform schrittweise zur Zielposition
                float moveStep = jumpHeight * Time.deltaTime; // Anpassen der Geschwindigkeit hier nach Bedarf
                _transform.position = Vector2.MoveTowards(_transform.position, targetPosition, moveStep);
            }
            else{
                Boss2BT.isJumping = false;
                Boss2BT.isFalling = true; 
            }
        }
        private void PerformFall()
        {
            if (elapsedTime > 0)
            {       elapsedTime -= Time.deltaTime;
                    Vector2 targetPosition = new Vector2(originalPosition.x, originalPosition.y + 3);
                // Bewege das Transform schrittweise zur Zielposition
                float moveStep = jumpHeight * Time.deltaTime; // Anpassen der Geschwindigkeit hier nach Bedarf
                _transform.position = Vector2.MoveTowards(_transform.position, originalPosition, moveStep);
            }
            else{
                Boss2BT.isFalling = false; 
                Boss2BT.dogdeReady = false; 
                elapsedTime = 0; 
            }
        }
}
}