using System.Collections.Generic;
using System.Text;

namespace Behaviour.Nodes {
    public class Node {
        public enum Status {
            Success = 0,
            Failure = 1,
            Running = 2
        }
        
        public readonly string name;
        
        public readonly List<Node> children;
        protected int _currentChild;
        
        public Node(string name = "Node") {
            this.name = name;
            this.children = new List<Node>();
        }
        
        public virtual void AddChild(Node child) => children.Add(child);
        
        public virtual Status Process() => children[_currentChild].Process();
        
        public virtual Status Process(StringBuilder stack) {
            Status status = children[_currentChild].Process(stack);
            stack.AppendLine("[Node] " + name + " status: " + status);
            return status;
        }
        
        public virtual void Reset() {
            _currentChild = 0;
            
            for (int childId = 0; childId < children.Count; childId++) {
                children[childId].Reset();
            }
        }
    }
}