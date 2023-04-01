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
            ai.NavAgent.speed = ai.Params.ChaseSpeed;
            ai.Emotions.SetEmotion(EmotionType.Angry);
            chaseTimer = maxChaseTime;
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.ChaseHash, false);
            ai.NavAgent.speed = ai.Params.WalkSpeed;
            ai.Emotions.SetEmotion(EmotionType.None);
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
            Systems.Instance.GameStateDispatcher.DispatchPlayerChased();

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