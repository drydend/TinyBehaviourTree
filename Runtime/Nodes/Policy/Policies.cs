namespace Behaviour.Nodes.Policy {
    public static class Policies {
        public static readonly IPolicy runForever = new RunForeverPolicy();
        public static readonly IPolicy runUntilSuccess = new RunUntilSuccessPolicy();
        public static readonly IPolicy runUntilFailure = new RunUntilFailurePolicy();
        
        public sealed class RunForeverPolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => false;
        }
        
        public sealed class RunUntilSuccessPolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Success;
        }
        
        public sealed class RunUntilFailurePolicy : IPolicy {
            public bool ShouldReturn(Node.Status status) => status == Node.Status.Failure;
        }
    }
}