using NursingHome.Interactions;
using NursingHome.Lures;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NursingHome.AI
{
    public class AI : MonoBehaviour
    {
        [SerializeField]
        LureDetector lureDetector;

        [field:SerializeField]
        public Animator Animator { get; private set; }
        [field: SerializeField]
        public NavMeshAgent NavAgent { get; private set; }

        //Todo:
        // Organize it into some params container
        [field: SerializeField]
        public float WalkSpeed { get; private set; } = 3.2f;
        [field: SerializeField]
        public float ChaseSpeed { get; private set; } = 5.0f;
        [field: SerializeField]
        public float InRangeDistance { get; private set; } = 0.2f;

        public Lure StrongestLure => lureProcessor.CurrentStrongestLure;
        LureProcessor lureProcessor;

        List<PrankParams> investigatedPranks = new List<PrankParams>();

        public void OnPrankInvestigated()
        {
            investigatedPranks.Add(StrongestLure.Prank);
            lureProcessor.ProcessNextPrank();            
        }

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