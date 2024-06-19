using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class Boss2BT : BehaviorTree.Tree{

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
            new List<Node> {
                /*
                new Sequence(                    
                    new List<Node>{
                        new CheckDogede(transform), 
                        new TaskToDogde(transform); 
                    }), 
                new Sequence(
                    new CheckPhase(transform),
                    new TaskToPhase2(transform)
                ),
                new Sequence(){
                    new CheckToFireBeam(); 
                    new TaskToFireBeam(transform); 
                } 
                */
         });        
        return root;
        }
        
    public bool LeftLooking(){
        return isLeft;
    }
}
