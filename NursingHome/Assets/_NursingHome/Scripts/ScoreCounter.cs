using NursingHome.Interactions;
using System.Collections.Generic;

namespace NursingHome
{
    public interface IScoreCounter
    {
        int GetTotalScore();
        int GetScoreEarnedForDay();
        int GetStartingScore();
        Dictionary<PrankParams, int> GetPranks();
    }

    public class ScoreCounter : IScoreCounter
    {
        Dictionary<PrankParams, int> pranksCount = new Dictionary<PrankParams, int>();
        int startingPoints;

        public ScoreCounter(ItemUser itemUser, int v)
        {
            itemUser.OnItemUsed += HandleItemUsed;
            this.startingPoints = v;
        }

        public Dictionary<PrankParams, int> GetPranks()
        {
            return pranksCount;
        }

        public int GetScoreEarnedForDay()
        {
            int totalScore = 0;

            foreach (var pair in pranksCount)
            {
                var prankScore = pair.Key.PrankPoints;
                var amount = pair.Value;

                totalScore += prankScore * amount;
            }

            return totalScore;
        }

        public int GetStartingScore()
        {
            return startingPoints;
        }

        public int GetTotalScore()
        {
            return GetStartingScore() + GetScoreEarnedForDay();
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