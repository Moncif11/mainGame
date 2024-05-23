using System.Collections.Generic;
using System.Diagnostics;

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
                        continue;
                    case NodeState.FAILURE:
                        state  = NodeState.FAILURE;
                        return state; 
                    case NodeState.RUNNING:
                        anyChildIsRunning = true;
                        return state; 
                    default:
                        state = NodeState.SUCCESS;
                        return state;
                    }
                }
                state = anyChildIsRunning ? NodeState.RUNNING: NodeState.SUCCESS;
                return state;
            } 
        }
}