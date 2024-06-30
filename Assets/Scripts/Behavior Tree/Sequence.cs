using System.Collections.Generic;
using UnityEngine;
namespace BehaviorTree{
    public class Sequence : Node
    {
        public Sequence() : base() { }
        public Sequence(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false; 
            foreach(Node node in children){
                switch(node.Evaluate()){
                    case NodeState.SUCCESS:
                        //Debug.Log("Sequence success.");
                        continue;
                    case NodeState.FAILURE:
                        state  = NodeState.FAILURE;
                        //Debug.Log("Sequence failed.");
                        return state; 
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        //Debug.Log("Sequence running.");
                         continue;
                    }
                }
               // Debug.Log("Sequence is empty");
                //return NodeState.SUCCESS;
                state = anyChildIsRunning ? NodeState.RUNNING: NodeState.SUCCESS;
               return state;
            } 
        }
}