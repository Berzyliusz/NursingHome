using NursingHome.Interactions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome
{
    public interface IScoreCounter
    {
        public int GetTotalScore();
    }

    public class ScoreCounter : IScoreCounter
    {
        Dictionary<PrankParams, int> pranksCount = new Dictionary<PrankParams, int>();

        public ScoreCounter(ItemUser itemUser)
        {
            itemUser.OnItemUsed += HandleItemUsed;
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

        void HandleItemUsed(ItemParams itemParams, PrankParams prankParams)
        {
            if(!pranksCount.ContainsKey(prankParams))
            {
                pranksCount.Add(prankParams, 1);
            }
            else
            {
                pranksCount[prankParams]++;
            }

            Debug.Log($"Total score: {GetTotalScore()}");
        }
    }
}