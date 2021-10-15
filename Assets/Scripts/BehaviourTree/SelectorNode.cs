using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace BehaviourTree
{
    public class SelectorNode : List<INode>, INode
    {
        public bool? Evaluate()
        {
            // Executes children from left to right
            foreach (var node in this)
            {
                switch (node.Evaluate())
                {
                    // Only advances to next child upon failure
                    case false: continue;
                    case null: return null;
                    case true: return true;
                    
                }
            }
            return false;
        }
    }
}

/* Joshua Torrington-Smith
 * 2021-09-22 */