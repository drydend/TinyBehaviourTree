using Behaviour.Nodes;
using UnityEngine;
using UnityEngine.AI;

namespace Behaviour.Strategy {
    public interface IStrategy {
        Node.Status Process();
        
        void Reset() { }
    }
}