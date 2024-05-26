using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine.UIElements;

public class Boss1BT : Tree{
     protected override Node SetupTree(){
        Node root = new Selector( new List<Node> {new Selector(new List<Node>{ //Phase2 addition to special move
            new Sequence(new List<Node>{
                new CheckHalfHP(transform), 
                new TaskSetStatsPhase2(transform),
            }),
            new Sequence(), //Move SP1 an Dash attack with amount of speed
             new Sequence(), //Move SP2 an Shoot Attack with a bigger ball 
        }),
        new Sequence(), // Normal Attacks
        new Selector (new List<Node>{
            new Sequence(),
            new Sequence()
            }) , //Shooting or going 
        //Going to the enemy;
     });
        return root;
        }
}
