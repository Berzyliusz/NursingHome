using NursingHome.Interactions;
using System.Collections.Generic;

namespace NursingHome
{
    public interface IScoreCounter
    {
        public int GetTotalScore();
        public Dictionary<PrankParams, int> GetPranks();
    }

    public class ScoreCounter : IScoreCounter
    {
        Dictionary<PrankParams, int> pranksCount = new Dictionary<PrankParams, int>();
        private int startomgPointss;

        public ScoreCounter(ItemUser itemUser, int v)
        {
            itemUser.OnItemUsed += HandleItemUsed;
            this.startomgPointss = v;
        }

        public Dictionary<PrankParams, int> GetPranks()
        {
            return pranksCount;
        }

        public int GetTotalScore()
        {
            int totalScore = 0;

            foreach(var pair in pranksCount)
            {
                var prankScore = pair.Key.PrankPoints;
                var amount = pair.Value;

                totalScore += prankScore * amount;
            }

            return totalScore;
        }

        void HandleItemUsed(UsableElement prankedElement, Item item, PrankParams prankParams)
        {
            if(!pranksCount.ContainsKey(prankParams))
            {
                pranksCount.Add(prankParams, 1);
            }
            else
            {
                pranksCount[prankParams]++;
            }
        }
    }
}