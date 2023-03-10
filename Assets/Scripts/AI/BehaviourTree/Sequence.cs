using System.Collections.Generic;


namespace BehaviourTree
{
    public class Sequence : Node
    {
        public Sequence(params Node[] children) : base(children)
        {
        }

        public override NodeState Evaluate(Node Root)
        {
            bool ChildRunning = false;
            foreach (Node node in children)
            {
                switch (node.Evaluate(Root))
                {
                    case NodeState.Failure:
                        state = NodeState.Failure;
                        return state;
                    case NodeState.Success:
                        continue;
                    case NodeState.Running:
                        ChildRunning = true;
                        continue;
                    case NodeState.Abort:
                        state = NodeState.Abort;
                        return state;
                    default:
                        state = NodeState.Success;
                        return state;
                }
            }

            state = (ChildRunning) ? NodeState.Running : NodeState.Success;
            return state;
        }
    }
}