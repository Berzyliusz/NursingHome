using NursingHome.Interactions;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome
{
    public interface IScoreCounter
    {

    }

    public class ScoreCounter : IScoreCounter
    {
        readonly ItemUser itemUser;

        Dictionary<PrankParams, int> pranksCount = new Dictionary<PrankParams, int>();

        public ScoreCounter(ItemUser itemUser)
        {
            this.itemUser = itemUser;
            itemUser.OnItemUsed += HandleItemUsed;
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

            Debug.Log($"Score for {prankParams.DisplayName} = {pranksCount[prankParams]}");
        }
    }
}