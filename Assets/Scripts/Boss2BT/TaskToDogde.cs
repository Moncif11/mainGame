using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

namespace Boss2AI{
public class TaskToDogde : Node
{private Transform _transform;
        private Transform _groundCheck; 
        private Rigidbody2D _rigidbody2D;
        private bool isJumping = false;
        private Vector2 originalPosition;
        private float jumpStartTime;
        private float jumpHeight = 5f; // Höhe des Sprungs
        private float jumpDuration = 0.5f; // Dauer des Sprungs in Sekunden
        private float groundCheckRadius = 0.1f; // Radius des Bodenkreises
        private LayerMask groundLayer; // LayerMask für den Boden 
    public TaskToDogde(Transform transform, Transform groundCheck,LayerMask groundLayer){
        _transform = transform;
        _groundCheck = groundCheck;
        this.groundLayer = groundLayer;
    }
        public override NodeState Evaluate(){
             bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, groundCheckRadius, groundLayer);
              if (isGrounded && !isJumping)
            {
                StartJump();
            }

            if (isJumping)
            {
                PerformJump();
            }

            // Wenn der Sprung abgeschlossen ist, kehren wir zum Erfolg zurück
            if (!isJumping && isGrounded)
            {   
                Boss2BT.dogdeReady = false;
            }
            else
            {
                state = NodeState.RUNNING;
            }

            return state;
        }

        private void StartJump()
        {
            isJumping = true;
            originalPosition = _transform.position;
            jumpStartTime = Time.time;
        }

        private void PerformJump()
        {
            float elapsedTime = Time.time - jumpStartTime;
            if (elapsedTime < jumpDuration)
            {
                // Berechnen der neuen Position basierend auf der parabolischen Bewegung
                float progress = elapsedTime / jumpDuration;
                float verticalOffset = Mathf.Sin(Mathf.PI * progress) * jumpHeight;
                _rigidbody2D.MovePosition(new Vector2(originalPosition.x, originalPosition.y + verticalOffset));
            }
            else
            {
                // Sprung beendet
                isJumping = false;
                _rigidbody2D.MovePosition(new Vector2(originalPosition.x, originalPosition.y));
            }
        }
    }
}