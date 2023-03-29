using NursingHome.Animations;
using System;
using UnityEngine;
using NursingHome.UserInterface;

namespace NursingHome.AI
{
    public class ChasePlayerState : StateBase, IState
    {
        [SerializeField]
        float maxChaseTime = 30f;
        [SerializeField]
        float distanceToGiveUpChase = 30.0f;
        [SerializeField]
        float distanceToCatch = 1.0f;

        float chaseTimer;
        float distanceToTarget;

        IPlayer player;

        public void StartState()
        {
            player = Systems.Instance.Player;
            ai.Animator.SetBool(AnimationHashes.ChaseHash, true);
            ai.NavAgent.speed = ai.ChaseSpeed;
            chaseTimer = maxChaseTime;
            Systems.Instance.GameStateDispatcher.DispatchPlayerChased();
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.ChaseHash, false);
            ai.NavAgent.speed = ai.WalkSpeed;
            chaseTimer = float.MaxValue;
            distanceToTarget = float.MaxValue;
        }

        public bool IsStateDone()
        {
            return chaseTimer <= 0 || distanceToTarget > distanceToGiveUpChase;
        }

        public void UpdateState()
        {
            chaseTimer -= Time.deltaTime;
            ai.NavAgent.SetDestination(player.GetPlayerPosition());

            distanceToTarget = Vector3.Distance(transform.position, player.GetPlayerPosition());

            if(distanceToTarget < distanceToCatch)
            {
                // TODO: Move this to some GameEnder
                EndGame();
            }
        }

        void EndGame()
        {
            var systems = Systems.Instance;
            systems.Time.SetTimeScale(0);
            player.SetFreezePlayer(true);
            systems.Cursor.SetCursorVisible(true);
            systems.Cursor.SetCursorLocked(CursorLockMode.None);
            systems.UISystem.ShowScreen(UIType.LooseScreen);
        }
    }
}