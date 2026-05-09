using System.Text;

namespace Behaviour.Nodes {
    public class UntilFail : AbortNode {
        public UntilFail(string name = "until fail", AbortType abortType = AbortType.None) : base(name, abortType) { }
        
        public override Status Process() {
            if (CheckAbort()) {
                return Status.Failure;
            }
            
            if (children[0].Process() == Status.Failure) {
                Reset();
                return Status.Failure;
            }
            
            return Status.Running;
        }
        
        public override Status Process(StringBuilder stack) {
            stack.AppendLine($"[UntilFail] + {name}");
            
            if (CheckAbort()) {
                return Status.Failure;
            }
            
            if (children[0].Process(stack) == Status.Failure) {
                Reset();
                return Status.Failure;
            }
            
            return Status.Running;
        }
    }
}