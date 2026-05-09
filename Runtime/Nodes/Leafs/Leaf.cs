using System.Text;
using Behaviour.Strategy;

namespace Behaviour.Nodes {
    public class Leaf : Node {
        protected readonly IStrategy _strategy;
        
        public Leaf(IStrategy strategy, string name = "leaf") : base(name) {
            this._strategy = strategy;
        }
        
        public override Status Process() => _strategy.Process();
        
        public override Status Process(StringBuilder stack) {
            Status status = Process();
            stack.AppendLine("[Leaf] " + name + " strategy: " + _strategy.GetType().Name + " status: " + status);
            return status;
        }
        
        public override void Reset() => _strategy.Reset();
    }
}