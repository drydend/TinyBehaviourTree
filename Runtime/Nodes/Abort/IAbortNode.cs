namespace Behaviour.Nodes {
    public interface IAbortNode {
        AbortType abortType { get; }
        
        Node.Status ProcessAbort();
    }
}