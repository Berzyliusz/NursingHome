using NursingHome.Lures;
using UnityEngine;

namespace NursingHome.AI
{
    public class LureProcessor
    {
        readonly LureDetector detector;

        public LureProcessor(LureDetector detector)
        {
            this.detector = detector;

            detector.OnLureEnter += HandleLureEntered;
            detector.OnLureExit += HandleLureExit;
        }

        void HandleLureEntered(Lure lure)
        {
            Debug.Log($"Lure ENTER {lure.Prank.DisplayName}");
        }

        void HandleLureExit(Lure lure)
        {
            Debug.Log($"Lure EXIT {lure.Prank.DisplayName}");
        }
    }
}