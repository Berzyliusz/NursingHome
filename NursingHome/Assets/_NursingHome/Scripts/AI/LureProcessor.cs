using NursingHome.Lures;
using System.Collections.Generic;

namespace NursingHome.AI
{
    public class LureProcessor
    {
        List<Lure> spottedLures = new List<Lure>();
        HashSet<Lure> processedLures = new HashSet<Lure>();

        public Lure CurrentStrongestLure { get; private set; }

        public LureProcessor(LureDetector detector)
        {
            detector.OnLureDetectionEnter += HandleLureDetectionEntered;
            detector.OnLureDetectionExit += HandleLureDetectionExit;
        }

        public void ProcessNextPrank()
        {
            spottedLures.Remove(CurrentStrongestLure);
            processedLures.Add(CurrentStrongestLure);
            CurrentStrongestLure = null;

            if (spottedLures.Count == 0)
                return;

            Lure strongestLure = spottedLures[0];

            foreach(var lure in spottedLures)
            {
                if (lure.Prank.PrankPoints > strongestLure.Prank.PrankPoints)
                    strongestLure = lure;
            }

            CurrentStrongestLure = strongestLure;
        }

        void HandleLureDetectionEntered(Lure lure)
        {
            if(processedLures.Contains(lure)) 
                return;

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