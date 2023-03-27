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

        public Animator Animator { get; private set; }
        public NavMeshAgent NavAgent { get; private set; }

        public Lure StrongestLure => lureProcessor.CurrentStrongestLure;
        LureProcessor lureProcessor;

        public bool HasLureDetected()
        {
            return lureProcessor.CurrentStrongestLure != null;
        }

        void Awake()
        {
            lureProcessor = new LureProcessor(lureDetector);

            Animator = GetComponent<Animator>();
            NavAgent = GetComponent<NavMeshAgent>();

            var states = GetComponents<StateBase>();
            foreach(var state in states)
            {
                state.SetupAI(this);
            }
        }        
    }
}