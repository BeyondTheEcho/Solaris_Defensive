using System;

namespace BehaviourTree
{
    public class NotNode : INode
    {
        public NotNode(INode child) => _child = child;
        private readonly INode _child;
        public bool? Evaluate()
        {
            switch (_child.Evaluate())
            {
                case false: return true;
                case true: return false;
                case null: return null;
            }

            throw new InvalidOperationException(
                $"This code should not be reachable but unity will complain if it's not here");
        }
    }
}

/* Joshua Torrington-Smith
 * 2021-09-22 */