using System.Collections.Generic;
using BehaviorTree;
using HealMonsterAI;

public class HealMonsterBT : Tree {

    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;

    public static float fovRange = 10f;

    public static float attackRange = 3f; 

    public UnityEngine.GameObject bulletPrefab; 

    public static bool isRight = true; 

    protected override Node SetupTree(){
        Node root = new Selector(
            new List<Node>{
                new Sequence( new List<Node>{
                            new CheckToHeal(transform),  
                            new TaskAttack(bulletPrefab,transform),
                       }),
               new Sequence( new List<Node>{
                            new CheckEnemyAttack(transform),  
                            new TaskAttack(bulletPrefab,transform),
                       }),
                    new Sequence(
                        new List<Node>{
                            new CheckEnemyInFOVRange(transform),  
                            new TaskToGo(transform),
                        }),
                    new TaskPatrol(transform, waypoints)

            });
            return root;
        }
}