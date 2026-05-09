namespace Behaviour.Nodes {
    public sealed class AbortNodeData {
        public readonly int childId;
        public readonly IAbortNode node;
        
        public AbortNodeData(int childId, IAbortNode node) {
            this.childId = childId;
            this.node = node;
        }
    }
}