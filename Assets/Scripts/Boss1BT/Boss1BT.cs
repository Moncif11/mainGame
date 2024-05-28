using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public enum BossPhase{
    ONE, 
    TWO,
}
public class Boss1BT : BehaviorTree.Tree{

    [Header("")]
    public static BossPhase bossPhase = BossPhase.ONE;
    [Header("Stats")] 
    public static float attack=10; 

    public static float speed=2f;

    [Header("Direction")]
    public static bool isLeft = true;  

    public static float meleeRange = 2f; 

    public static float shootRange = 5f;
     protected override Node SetupTree(){
        Node root = 
        new Selector( 
            new List<Node> {new Selector(new List<Node>{ //Phase2 addition to special move
            new Sequence(new List<Node>{
                new CheckHalfHP(transform), 
                new TaskSetStatsPhase2(transform),
            }),
            //new Sequence(), //Move SP1 an Dash attack with amount of speed
            // new Sequence(), //Move SP2 an Shoot Attack with a bigger ball 
        }),
        //new Sequence(), // Normal Attacks
        /*new Selector (new List<Node>{
            new Sequence(),//Shooting
            new Sequence()  //going to AttackRange
            }) ,    
            */
        new TaskToGoB1(transform),      //Going to the enemy;
         });
        return root;
        }
}
