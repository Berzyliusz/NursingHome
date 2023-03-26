using NursingHome.Interactions;
using System;

namespace NursingHome
{
    public interface IScoreCounter
    {

    }

    public class ScoreCounter : IScoreCounter
    {
        readonly ItemUser itemUser;

        public ScoreCounter(ItemUser itemUser)
        {
            this.itemUser = itemUser;
            itemUser.OnItemUsed += HandleItemUsed;
        }

        void HandleItemUsed(ItemParams itemParams, PrankParams prankParams)
        {
            
        }
    }
}