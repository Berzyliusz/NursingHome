using NursingHome.Interactions;
using NursingHome.Lures;
using Sirenix.OdinInspector;
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

        [SerializeField]
        Transform eyesTransform;
        [SerializeField]
        Transform emotionIconTransform;

        [SerializeField]
        [InlineEditor]
        AIParams aiParams;

        public AIParams Params => aiParams;

        public Lure StrongestLure => lureProcessor.CurrentStrongestLure;
        LureProcessor lureProcessor;

        public IEyes Eyes { get ; private set; }
        public IEmotionDisplayer Emotions { get; private set; }

        List<PrankParams> investigatedPranks = new List<PrankParams>();

        IUpdateable[] updateables;

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

        void Start()
        {
            lureProcessor = new LureProcessor(lureDetector);
            Eyes = new AiEyes(Systems.Instance.Player, aiParams, eyesTransform);
            Emotions = new AiEmotionDisplayer(emotionIconTransform, aiParams.Emotions);

            updateables = new IUpdateable[2];
            updateables[0] = Eyes;
            updateables[1] = Emotions;
        }

        void Update()
        {
            foreach(var updatable in updateables)
            {
                updatable.Update(Time.deltaTime);
            }
        }
    }
}