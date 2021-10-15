using System.Collections.Generic;

namespace BehaviourTree
{
    public class SequenceNode : List<INode>, INode
    {
        public bool? Evaluate()
        {
            // Executes children from left to right
            foreach (var node in this)
            {
                switch (node.Evaluate())
                {
                    // Only advances to next child upon success
                    case true: continue;
                    case false: return false;
                    case null: return null;
                }
            }
            return true;
        }
    }
}

/* Joshua Torrington-Smith
 * 2021-09-22 */
