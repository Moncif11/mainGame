using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace BehaviorTree{
    public class InverterDecorator : Node
    {
        Node _children; 
        public InverterDecorator() : base() { }
        public InverterDecorator (Node children) {
            _children = children;
         }
        public override NodeState Evaluate()
        {
                switch(_children.Evaluate()){
                    case NodeState.SUCCESS:
                        state = NodeState.FAILURE;
                        return state;
                    case NodeState.RUNNING:
                        state = NodeState.RUNNING;
                        return state;                         
                    case NodeState.FAILURE:
                        state = NodeState.SUCCESS;
                        return state; 
                    default:
                    return state; 
                    } 
                }
            } 
}