using System.Text;
using Behaviour.Nodes.Policy;
using UnityEngine;

namespace Behaviour.Nodes {
    public class BehaviourTree : Node {
        private readonly IPolicy _policy;
        
        public BehaviourTree(string name = "Tree", IPolicy policy = null) : base(name) {
            this._policy = policy ?? Policies.runForever;
        }
        
        public override Status Process() {
            Status status = children[_currentChild].Process();
            
            if (_policy.ShouldReturn(status)) {
                return status;
            }
            
            _currentChild = (_currentChild + 1) % children.Count;
            return Status.Running;
        }
        
        public override Status Process(StringBuilder stack) {
            stack.AppendLine("[BehaviourTree] " + name);
            Status status = children[_currentChild].Process(stack);
            
            if (_policy.ShouldReturn(status)) {
                return status;
            }
            
            _currentChild = (_currentChild + 1) % children.Count;
            return Status.Running;
        }
        
        public void PrintTree() {
            StringBuilder sb = new StringBuilder();
            PrintNode(this, 0, sb);
            Debug.Log(sb.ToString());
        }
        
        private static void PrintNode(Node node, int indentLevel, StringBuilder sb) {
            sb.Append(' ', indentLevel * 2).AppendLine(node.name);
            
            for (int childId = 0; childId < node.children.Count; childId++) {
                PrintNode(node.children[childId], indentLevel + 1, sb);
            }
        }
    }
}