using NursingHome.Lures;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.AI
{
    public class LureProcessor
    {
        public event Action<Lure> OnStrongestLureSet;

        readonly LureDetector detector;
        List<Lure> spottedLures = new List<Lure>();

        Lure currentStrongestLure;

        public LureProcessor(LureDetector detector)
        {
            this.detector = detector;

            detector.OnLureDetectionEnter += HandleLureDetectionEntered;
            detector.OnLureDetectionExit += HandleLureDetectionExit;
        }

        void HandleLureDetectionEntered(Lure lure)
        {
            spottedLures.Add(lure);

            if(currentStrongestLure== null)
            {
                currentStrongestLure = lure;
                OnStrongestLureSet?.Invoke(currentStrongestLure);
            }
            else
            {
                if(lure.Prank.PrankPoints > currentStrongestLure.Prank.PrankPoints)
                {
                    currentStrongestLure = lure;
                    OnStrongestLureSet?.Invoke(currentStrongestLure);
                }
            }

        }

        void HandleLureDetectionExit(Lure lure)
        {
            // we dont want to remove lures -> we remember the spotted ones
            // and we can investigate them later
        }
    }
}