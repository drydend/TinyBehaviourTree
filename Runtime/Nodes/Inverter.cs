using System.Text;

namespace Behaviour.Nodes {
    public class Inverter : Node {
        public Inverter(Node node, string name) : base(name) {
            children.Add(node);
        }
        
        public override Status Process() {
            switch (children[0].Process()) {
                case Status.Running:
                    return Status.Running;
                
                case Status.Failure:
                    return Status.Success;
                
                default:
                    return Status.Failure;
            }
        }
        
        public override Status Process(StringBuilder stack) {
            Status status = Process();
            stack.AppendLine("[Inverter] " + name + " status: " + status);
            return status;
        }
    }
}