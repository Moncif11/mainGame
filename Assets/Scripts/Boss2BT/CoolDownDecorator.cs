using UnityEngine;
using BehaviorTree;

namespace Boss2AI{
public class CoolDownDecorator: Node
{   
    Node _child; 
    public CoolDownDecorator(Node child){
        _child = child;
    }

        public override NodeState Evaluate()
        {   
            if(!Boss2BT.SP1Ready){
                 Boss2BT.coolDownSP1Counter += Time.deltaTime; 
                if(Boss2BT.coolDownSP1Counter>Boss2BT.coolDownSP1){
                    Boss2BT.amountShoot = 3; 
                    Boss2BT.coolDownSP1Counter= 0;
                    Boss2BT.SP1Ready= true;
                }
            }
            if(!Boss2BT.SP2Ready){
                 Boss2BT.coolDownSP2Counter += Time.deltaTime; 
                if(Boss2BT.coolDownSP2Counter>Boss2BT.coolDownSP2){
                     Boss2BT.coolDownSP2Counter= 0;
                    Boss2BT.SP2Ready= true;
                }
            }
             if(!Boss2BT.dogdeReady){
                 Boss2BT.coolDownDodgeCounter += Time.deltaTime; 
                if(Boss2BT.coolDownDodgeCounter>3.0f){
                     Boss2BT.coolDownDodgeCounter= 0;
                    Boss2BT.dogdeReady= true;
                }

            }
            return _child.Evaluate();
        }

    }
}