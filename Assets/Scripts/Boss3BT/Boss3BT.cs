using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
using Boss3AI;
public class Boss3BT : BehaviorTree.Tree{

    [Header("")]
    public static BossPhase bossPhase = BossPhase.ONE;
    [Header("Stats")] 
    public static float attack=10; 

    public static float speed=3f;

    public GameObject normalBulletPrefab; 

    public GameObject burnPrefab;

    [Header("Direction")]
    public static bool isLeft = true;  

    public static float attackRange = 4.5f;

    public static float coolDownSP1 = 5f; //Shoot Missile
    
    public static float coolDownSP2 = 7.5f; //Laser
    public static float coolDownDodgeCounter = 0f;
     public static float coolDownSP2Counter = 0f;
    public static float coolDownSP1Counter = 0f; 

    public static bool teleportReady = true; 
    public static bool SP1Ready=true; 
    public static bool SP2Ready=true;

    public static bool backToStart = false; 
       protected override Node SetupTree(){
         Node root = new CoolDownDecorator(
            new Selector(
                new List<Node> {
                    new Sequence(new List<Node> {
                        new CheckEnviroment(transform),
                        new Selector(new List<Node> {
                            new Sequence(new List<Node> {
                                new CheckToTeleport(transform),
                                new TaskToTeleport(transform)
                                }), 
                            new Sequence(new List<Node> {
                                new CheckCurrentPhase(transform, BossPhase.THREE),
                                new Sequence(new List<Node> {
                                    new CheckToIceRain(transform),
                                    new TaskToIceRain(transform, normalBulletPrefab),
                                }) 
                                }),
                                new Sequence(new List<Node> {
                                    new CheckBeginPhase(transform, BossPhase.TWO),
                                    new TaskToNextPhase(transform, BossPhase.THREE)
                                }),
                                new Sequence(new List<Node> {
                                    new CheckCurrentPhase(transform, BossPhase.TWO),
                                    new Sequence(new List<Node> {
                                        new CheckToBurn(transform),
                                        new TaskToBurn(transform, burnPrefab),
                                    })
                                }),
                                new Sequence(new List<Node> {
                                    new CheckBeginPhase(transform, BossPhase.ONE),
                                    new TaskToNextPhase(transform, BossPhase.TWO)
                                }),
                                new Sequence(new List<Node> {
                                    new CheckToMeleeAttack(transform),
                                    new TaskToMeleeAttack(transform)
                                }),
                                new Sequence(new List<Node> {
                                    new CheckToGoToAttackRange(transform),
                                    new TaskToGoToAttackRange(transform)
                                })
                })
            }),
            new TaskToBackToPoint(transform)
        }
        )
    );
    return root;
     }
        
    public bool LeftLooking(){
        return isLeft;
    }
}