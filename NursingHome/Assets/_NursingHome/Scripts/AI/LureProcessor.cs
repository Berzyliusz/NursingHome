using NursingHome.Lures;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.AI
{
    public class LureProcessor
    {
        readonly LureDetector detector;
        List<Lure> lures = new List<Lure>();

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