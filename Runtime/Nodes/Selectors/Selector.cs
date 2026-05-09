using System.Text;

namespace Behaviour.Nodes {
    public partial class Selector : AbortNode {
        public Selector(string name = "default selector", AbortType abortType = AbortType.None) : base(name, abortType) { }
        
        
        public override Status Process() {
            if (CheckAbort()) {
                return Status.Failure;
            }
            
            ProcessHighPriorityNodes();
            
            if (_currentChild < children.Count) {
                switch (children[_currentChild].Process()) {
                    case Status.Running:
                        return Status.Running;
                    
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    
                    default:
                        _currentChild++;
                        return Status.Running;
                }
            }
            
            Reset();
            return Status.Failure;
        }
        
        public override Status Process(StringBuilder stack) {
            stack.AppendLine($"[Selector] {name}:");
            
            if (CheckAbort()) {
                stack.AppendLine($"[Selector] {name} abort");
                return Status.Failure;
            }
            
            ProcessHighPriorityNodes();
            
            if (_currentChild < children.Count) {
                switch (children[_currentChild].Process(stack)) {
                    case Status.Running:
                        return Status.Running;
                    
                    case Status.Success:
                        Reset();
                        return Status.Success;
                    
                    default:
                        _currentChild++;
                        return Status.Running;
                }
            }
            
            stack.AppendLine($"[Selector] {name} status: {Status.Failure}");
            Reset();
            return Status.Failure;
        }
    }
}