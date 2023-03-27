using UnityEngine;

namespace NursingHome.AI
{
    public class ChasePlayerState : StateBase, IState
    {
        IPlayer player;

        public void StartState()
        {
            // walk / run anim
            Debug.Log("CHASE");
            player = Systems.Instance.Player;
        }

        public void EndState()
        {
            // stop walk anim
        }

        public bool IsStateDone()
        {
            return false;
        }

        public void UpdateState()
        {
            ai.NavAgent.SetDestination(player.GetPlayerPosition());
        }
    }
}