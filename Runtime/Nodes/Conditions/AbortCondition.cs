using System;
using System.Text;

namespace Behaviour.Nodes {
    public class AbortCondition : Node, IAbortNode {
        public AbortType abortType => AbortType.None;
        
        private readonly Func<bool> _predicate;
        
        public AbortCondition(Func<bool> predicate, string name = "condition") : base(name) {
            _predicate = predicate;
        }
        
        public override Status Process() => _predicate() ? Status.Success : Status.Failure;
        
        public override Status Process(StringBuilder stack) {
            Status status = Process();
            stack.AppendLine("[AbortCondition] " + name + " status: " + status);
            return status;
        }
        
        
        public Status ProcessAbort() => _predicate() ? Status.Failure : Status.Success;
    }
}