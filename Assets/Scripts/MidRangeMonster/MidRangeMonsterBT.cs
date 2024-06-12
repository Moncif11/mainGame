using System.Collections.Generic;
using BehaviorTree;
using MidRangeMonsterAI; 

public class MidRangeMonsterBT : Tree {

    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;

    public static float fovRange = 10f;

    public static float attackRange = 2f; 

    public static bool isRight = true; 

    protected override Node SetupTree(){
        Node root = new Selector(
            new List<Node>{
                new Sequence( new List<Node>{
                            new CheckGuard(transform),  
                            new TaskToGuard(transform),
                       }),
               new Sequence( new List<Node>{
                            new CheckEnemyAttack(transform),  
                            new TaskAttack(transform),
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