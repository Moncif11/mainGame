using System.Collections.Generic;
using BehaviorTree;

public class GuardBT : Tree {

    public UnityEngine.Transform[] waypoints;
    public static float speed = 2f;

    public static float fovRange = 6f;

    public static float attackRange = 2f; 

    protected override Node SetupTree(){
        Node root = new Selector(
            new List<Node>{
                new Sequence( new List<Node>{
                            new CheckEnemyAttack(transform),  
                            new TaskAttack(transform),
                        }),
                    new Sequence(
                        new List<Node>{
                            new CheckEnemyInFOVRange(transform),  
                            new TaskToGo(transform),
                        }),
                        new TaskPatrol(transform, waypoints),
            });
            return root;
        }
}