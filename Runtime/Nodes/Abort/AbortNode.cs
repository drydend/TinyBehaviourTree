using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Behaviour.Nodes {
    public abstract class AbortNode : Node, IAbortNode {
        public AbortType abortType => _abortType;
        
        protected readonly AbortType _abortType;
        protected readonly List<AbortNodeData> _abortConditions;
        protected readonly List<AbortNodeData> _highPriorityNodes;
        
        protected AbortNode(string name = "abort node", AbortType abortType = AbortType.None) : base(name) {
            _abortType = abortType;
            _abortConditions = new List<AbortNodeData>();
            _highPriorityNodes = new List<AbortNodeData>();
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected bool CheckAbort() {
            if (_abortType == AbortType.Self || _abortType == AbortType.Both) {
                for (int id = 0; id < _abortConditions.Count; id++) {
                    if (_abortConditions[id].node.ProcessAbort() == Status.Success) {
                        Reset();
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void ProcessHighPriorityNodes() {
            for (int nodeId = 0; nodeId < _highPriorityNodes.Count; nodeId++) {
                if (_highPriorityNodes[nodeId].childId > _currentChild) {
                    break;
                }
                
                if (_highPriorityNodes[nodeId].childId == _currentChild) {
                    continue;
                }
                
                if (_highPriorityNodes[nodeId].node.ProcessAbort() == Status.Failure) {
                    Reset();
                    _currentChild = _highPriorityNodes[nodeId].childId;
                    break;
                }
            }
        }
        
        public Status ProcessAbort() {
            for (int id = 0; id < _abortConditions.Count; id++) {
                if (_abortConditions[id].node.ProcessAbort() == Status.Success) {
                    Reset();
                    return Status.Success;
                }
            }
            
            return Status.Failure;
        }
        
        public override void AddChild(Node child) {
            base.AddChild(child);
            
            if (child is IAbortNode condition) {
                AbortNodeData abortNodeData = new AbortNodeData(children.Count - 1, condition);
                _abortConditions.Add(abortNodeData);
                
                if (condition.abortType == AbortType.LowerPriority || condition.abortType == AbortType.Both) {
                    _highPriorityNodes.Add(abortNodeData);
                }
            }
        }
    }
}