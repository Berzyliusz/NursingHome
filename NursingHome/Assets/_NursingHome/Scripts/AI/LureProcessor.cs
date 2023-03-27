using NursingHome.Lures;
using System.Collections.Generic;

namespace NursingHome.AI
{
    public class LureProcessor
    {
        List<Lure> spottedLures = new List<Lure>();

        public Lure CurrentStrongestLure { get; private set; }

        public LureProcessor(LureDetector detector)
        {
            detector.OnLureDetectionEnter += HandleLureDetectionEntered;
            detector.OnLureDetectionExit += HandleLureDetectionExit;
        }

        void HandleLureDetectionEntered(Lure lure)
        {
            spottedLures.Add(lure);

            if(CurrentStrongestLure== null)
            {
                CurrentStrongestLure = lure;
            }
            else
            {
                if(lure.Prank.PrankPoints > CurrentStrongestLure.Prank.PrankPoints)
                {
                    CurrentStrongestLure = lure;
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