using NursingHome.Interactions;
using Opencoding.CommandHandlerSystem;
using System.Collections.Generic;
using UnityEngine;

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

        public ScoreCounter(ItemUser itemUser, int startingPoints)
        {
            itemUser.OnItemUsed += HandleItemUsed;
            this.startingPoints = startingPoints;

            CommandHandlers.RegisterCommandHandlers(this);
            //Todo: We will need to nicely unregister this command handler.
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

        [CommandHandler(Description = "Resets saved points, current day points, performed pranks. All goes back to zero.")]
        void ResetAllPoints()
        {
            Debug.Log("Reset points command heard");
            pranksCount.Clear();
            startingPoints = 0;
            Systems.Instance.SaveLoadSystem.SavePoints(0);
        }
    }
}