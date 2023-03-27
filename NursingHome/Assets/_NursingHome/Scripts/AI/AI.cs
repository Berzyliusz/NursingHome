using NursingHome.Lures;
using UnityEngine;

namespace NursingHome.AI
{
    public class AI : MonoBehaviour
    {
        [SerializeField]
        LureDetector lureDetector;
        [field:SerializeField]
        public float InRangeDistance { get; private set; } = 0.2f;

        public Lure StrongestLure => lureProcessor.CurrentStrongestLure;

        LureProcessor lureProcessor;

        void Awake()
        {
            lureProcessor = new LureProcessor(lureDetector);
        }


    }
}