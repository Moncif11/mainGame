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

    public GameObject normalBulletPrefab; 

    [Header("Direction")]
    public static bool isLeft = true;  

    public static float meleeRange = 2f; 

    public static float shootRange = 5f;

    public static int amountShoot = 3; 

    public static float coolDownSA = 5f; 

    
    public static float coolDownSP1 = 10f; 
    
    public static float coolDownSP2 = 7.5f; 

    public static float coolDownSACounter = 0f;
     public static float coolDownSP2Counter = 0f;
    public static float coolDownSP1Counter = 0f; 

    public static bool SP1Ready=true; 
    public static bool SP2Ready=true;
 
     protected override Node SetupTree(){
        Node root = 
        new Selector( 
            new List<Node> {new Selector(new List<Node>{ //Phase2 addition to special move
            new Sequence(new List<Node>{
                new CheckHalfHP(transform), 
                new TaskSetStatsPhase2(transform),
            }),
            /*new Sequence(
                new List<Node>{
                      new CheckCooldown(coolDownSP1, ref lastSP1Time),
                    new CheckSP1Ready(transform),
                     new TaskToSP1(transform),
                }
            ), //Move SP1 an Dash attack with amount of speed
            */
            /* new Sequence(
                new List<Node>{
                      new CheckCooldown(coolDownSP2, ref lastSP2Time),
                    new CheckSP2Ready(transform),
                     new TaskToSP2(transform),
                }
            ), *///Move SP2 an Shoot Attack with a bigger ball 
        }),
        new Sequence(new List<Node>{
             new CheckMeleeAttack(transform), 
            new TaskToMeleeAttack(transform)
            }
        ),// Normal Attacks
        new Selector (new List<Node>{
            new Sequence(new List<Node>{
                new CheckShooting(transform),  
                new TaskToShoot(transform),
                } 
            ),//Shooting
            new Sequence(
                new List<Node>{
                new CheckGoToAttackRange(transform),
                new TaskToGoToAttackRange(transform),
                }
            ) //going to AttackRange
            }) ,
            
        new TaskToGoShootRange(transform),      //Going to the enemy;
         });
        return root;
        }
    public bool LeftLooking(){
        return isLeft;
    }
}
