using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;
namespace Boss2AI{
public class Boss2BT : BehaviorTree.Tree{

    [Header("")]
    public static BossPhase bossPhase = BossPhase.ONE;
    [Header("Stats")] 
    public static float attack=10; 

    public static float speed=3f;

    public GameObject normalBulletPrefab; 

    [Header("Direction")]
    public static bool isLeft = true;  

    public static float shootRange = 5f;

    public static int amountShoot = 3;  

    public static float coolDownSP1 = 10f; 
    
    public static float coolDownSP2 = 7.5f; 

    public static float minDistanceLaser = 4;
    public static float maxDistanceLaser = 10.5f;

    public static float coolDownSACounter = 0f;
     public static float coolDownSP2Counter = 0f;
    public static float coolDownSP1Counter = 0f; 

    public static bool SP1Ready=true; 
    public static bool SP2Ready=true;
    
     protected override Node SetupTree(){
        Node root = 
          /* new Selector( 
            new List<Node> {
                new Sequence(new List<Node>{
                new CheckEnviroment(Transform),
                new Selector(new List<Node>{
                new Sequence(                    
                    new List<Node>{
                        new CheckDogede(transform), 
                        new TaskToDogde(transform); 
                    }), 
                new Sequence(
                    new CheckBeginPhase(transform,BossPhase.TWO),
                    new TaskToNextPhase(transform,BossPhase.Three)
                )
                ,
                new Sequence (new List<Node>{CheckCurrentPhase(transform,BossPhase.TWO),
                            new Sequence(new List<Node>{
                                      new CheckToShoot(transform),
                                     new TaskToShoottransform,Rocket),
                                    }); 
                                    }),
                new Sequence(
                    new CheckBeginPhase(transform,BossPhase.ONE),
                    new TaskToNextPhase(transform)
                )
                ,
                new Sequence (new List<Node>{CheckToPhase2(transform,BossPhase.TWO),
                            */new Sequence(new List<Node>{
                                      new CheckToFireBeam(transform),
                                     new TaskToFireBeam(transform),
                                    });/* 
                                    }),
                new Sequence(new List<Node>{
                            new CheckToShot(transform), 
                            new TaskToShoot(transform,bullet)
                })
                new Sequence(new List<Node>{ 
                            new CheckToGoToShootRange(transform), 
                            new TaskekToGoToShootRange(transform)
ä                })
                })
                })
                new TaskToGoBackToPoint(transform,speed)
         }); */        
        return root;
        }
        
    public bool LeftLooking(){
        return isLeft;
    }
}
}