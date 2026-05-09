using System;
using System.Text;

namespace Behaviour.Nodes {
    public class Condition : Node {
        private readonly Func<bool> _predicate;
        
        public Condition(Func<bool> predicate, string name = "condition") : base(name) {
            _predicate = predicate;
        }
        
        public override Status Process() => _predicate() ? Status.Success : Status.Failure;
        
        public override Status Process(StringBuilder stack) {
            stack.AppendLine("[Condition] " + name);
            return Process();
        }
    }
}