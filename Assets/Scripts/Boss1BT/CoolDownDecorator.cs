using UnityEngine;
using BehaviorTree;

namespace Boss1AI{
public class CoolDownDecorator: Node
{   
    Node _child; 
    public CoolDownDecorator(Node child){
        _child = child;
    }

        public override NodeState Evaluate()
        {   
            if(!Boss1BT.SP1Ready){
                 Boss1BT.coolDownSP1Counter += Time.deltaTime; 
                if(Boss1BT.coolDownSP1Counter>Boss1BT.coolDownSP1){
                    Boss1BT.coolDownSP1Counter= 0;
                    Boss1BT.SP1Ready= true;
                }
            }
            if(!Boss1BT.SP2Ready){
                 Boss1BT.coolDownSP2Counter += Time.deltaTime; 
                if(Boss1BT.coolDownSP2Counter>Boss1BT.coolDownSP2){
                     Boss1BT.coolDownSP2Counter= 0;
                    Boss1BT.SP2Ready= true;
                }
            }
            if(Boss1BT.amountShoot<= 0){
            Boss1BT.coolDownSACounter+= Time.deltaTime;
            if(Boss1BT.coolDownSACounter>=Boss1BT.coolDownSA){
                Boss1BT.coolDownSACounter=0;
                Boss1BT.amountShoot = (Boss1BT.bossPhase == BossPhase.ONE) ? 3 : 5;  
            }   
        }
            return _child.Evaluate();
        }

    }
}