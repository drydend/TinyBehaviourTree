using System.Text;

namespace Behaviour.Nodes {
    public class Sequence : AbortNode {
        public Sequence(string name = "sequence", AbortType abortType = AbortType.None) : base(name, abortType) { }
        
        public override Status Process() {
            if (CheckAbort()) {
                return Status.Failure;
            }
            
            ProcessHighPriorityNodes();
            
            if (_currentChild < children.Count) {
                switch (children[_currentChild].Process()) {
                    case Status.Running:
                        return Status.Running;
                    
                    case Status.Failure:
                        _currentChild = 0;
                        return Status.Failure;
                    
                    default:
                        _currentChild++;
                        return _currentChild == children.Count ? Status.Success : Status.Running;
                }
            }
            
            Reset();
            return Status.Success;
        }
        
        public override Status Process(StringBuilder stack) {
            stack.AppendLine($"[Sequence] {name}:");
            
            if (CheckAbort()) {
                stack.AppendLine($"[Sequence] {name} abort");
                return Status.Failure;
            }
            
            ProcessHighPriorityNodes();
            
            if (_currentChild < children.Count) {
                switch (children[_currentChild].Process(stack)) {
                    case Status.Running:
                        return Status.Running;
                    
                    case Status.Failure:
                        _currentChild = 0;
                        return Status.Failure;
                    
                    default:
                        _currentChild++;
                        return _currentChild == children.Count ? Status.Success : Status.Running;
                }
            }
            
            stack.AppendLine($"[Selector] {name} status: {Status.Success}");
            Reset();
            return Status.Success;
        }
    }
}