using System;
using BehaviorTree;

using UnityEngine;

namespace HealMonsterAI {
    public class CheckEnemyAttack : Node {
        Transform _transform;

        bool isFacing = false;

        public CheckEnemyAttack(Transform transform) {
            _transform = transform;
        }

        public override NodeState Evaluate() {
            try {
                object t = GetData("target");
                Transform target = (Transform)t;
                if (t == null) {
                    state = NodeState.FAILURE;
                    return state;
                }

                Vector2 direction = (_transform.position - target.position).normalized;
                if (direction.x < 0 && HealMonsterBT.isRight) {
                    isFacing = true;
                }
                else if (direction.x > 0 && !HealMonsterBT.isRight) {
                    isFacing = true;
                }
                else {
                    isFacing = false;
                }

                if (Vector2.Distance(_transform.position, target.position) <= HealMonsterBT.attackRange && isFacing) {
                    Debug.Log("heal monster shoot");
                    state = NodeState.SUCCESS;
                    return state;
                }

                state = NodeState.FAILURE;
                return state;
            }
            catch (Exception e) {
                state = NodeState.FAILURE;
                return state;
            }

        }
    }
}