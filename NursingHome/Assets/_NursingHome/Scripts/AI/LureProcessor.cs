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

            detector.OnLureEnter += HandleLureEntered;
            detector.OnLureExit += HandleLureExit;
        }

        void HandleLureEntered(Lure lure)
        {
            spottedLures.Add(lure);

            if(currentStrongestLure== null)
            {
                currentStrongestLure = lure;
                OnStrongestLureSet?.Invoke(currentStrongestLure);
                Debug.Log($"New lure set {lure.Prank.DisplayName}");
            }
            else
            {
                if(lure.Prank.PrankPoints > currentStrongestLure.Prank.PrankPoints)
                {
                    currentStrongestLure = lure;
                    OnStrongestLureSet?.Invoke(currentStrongestLure);
                    Debug.Log($"Stronger lure set {lure.Prank.DisplayName}");
                }
            }

        }

        void HandleLureExit(Lure lure)
        {
            // we dont want to remove lures -> we remember the spotted ones
            // and we can investigate them later
        }
    }
}