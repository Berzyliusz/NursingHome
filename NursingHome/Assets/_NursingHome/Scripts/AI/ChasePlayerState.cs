using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class ChasePlayerState : StateBase, IState
    {
        [SerializeField]
        float maxChaseTime = 30f;
        [SerializeField]
        float distanceToGiveUpChase = 30.0f;

        float chaseTimer;
        float distanceToTarget;

        IPlayer player;

        public void StartState()
        {
            Debug.Log("CHASE");
            player = Systems.Instance.Player;
            ai.Animator.SetBool(AnimationHashes.ChaseHash, true);
            chaseTimer = maxChaseTime;
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.ChaseHash, false);

            chaseTimer = float.MaxValue;
            distanceToTarget = float.MaxValue;
        }

        public bool IsStateDone()
        {
            return chaseTimer >= 0 || distanceToTarget > distanceToGiveUpChase;
        }

        public void UpdateState()
        {
            chaseTimer -= Time.deltaTime;
            ai.NavAgent.SetDestination(player.GetPlayerPosition());

            distanceToTarget = Vector3.Distance(transform.position, player.GetPlayerPosition());
        }
    }
}