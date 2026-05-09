namespace Behaviour.Nodes.Policy {
    public interface IPolicy {
        bool ShouldReturn(Node.Status status);
    }
}