using UnityEngine;
using BehaviorTree;

namespace Boss3AI{
public class CoolDownDecorator: Node
{   
    Node _child; 
    public CoolDownDecorator(Node child){
        _child = child;
    }

        public override NodeState Evaluate()
        {   
            if(!Boss3BT.SP1Ready){
                 Boss3BT.coolDownSP1Counter += Time.deltaTime; 
                if(Boss3BT.coolDownSP1Counter>Boss3BT.coolDownSP1){
                    Boss3BT.coolDownSP1Counter= 0;
                    Boss3BT.SP1Ready= true;
                }
            }
            if(!Boss3BT.SP2Ready){
                 Boss3BT.coolDownSP2Counter += Time.deltaTime; 
                if(Boss3BT.coolDownSP2Counter>Boss3BT.coolDownSP2){
                     Boss3BT.coolDownSP2Counter= 0;
                    Boss3BT.SP2Ready= true;
                }
            }
             if(!Boss3BT.teleportReady){
                 Boss3BT.coolDownDodgeCounter += Time.deltaTime; 
                if(Boss2BT.coolDownDodgeCounter>3.0f){
                     Boss2BT.coolDownDodgeCounter= 0;
                    Boss2BT.dogdeReady= true;
                }

            }
            return _child.Evaluate();
        }

    }
}