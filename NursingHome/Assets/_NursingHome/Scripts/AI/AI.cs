using NursingHome.Lures;
using UnityEngine;
using UnityEngine.AI;

namespace NursingHome.AI
{
    public class AI : MonoBehaviour
    {
        [SerializeField]
        LureDetector lureDetector;
        [field:SerializeField]
        public float InRangeDistance { get; private set; } = 0.2f;

        [field:SerializeField]
        public Animator Animator { get; private set; }
        [field: SerializeField]
        public NavMeshAgent NavAgent { get; private set; }

        public Lure StrongestLure => lureProcessor.CurrentStrongestLure;
        LureProcessor lureProcessor;

        public bool HasLureDetected()
        {
            return lureProcessor.CurrentStrongestLure != null;
        }

        public void SetupStates()
        {
            var states = GetComponents<StateBase>();
            foreach (var state in states)
            {
                state.SetupAI(this);
            }
        }

        void Awake()
        {
            lureProcessor = new LureProcessor(lureDetector);
        }
    }
}