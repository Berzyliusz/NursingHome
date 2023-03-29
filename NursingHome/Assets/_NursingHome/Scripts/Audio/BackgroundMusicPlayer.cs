using System;
using UnityEngine;

namespace NursingHome.Audio
{
    public interface IBackgroundMusicPlayer : IUpdateable
    {

    }

    public class BackgroundMusicPlayer : IBackgroundMusicPlayer
    {
        enum BackgroundMusicType
        {
            Exploration = 0,
            Searching = 1,
            Chase = 2
        };

        const float musicCooldownTime = 5;
        float cooldownTimer = 0;

        IGameStateDispatcher gameStateDispatcher;
        BackgroundMusicType currentMusicType;

        public BackgroundMusicPlayer(IGameStateDispatcher gameStateDispatcher)
        {
            this.gameStateDispatcher = gameStateDispatcher;

            // We should get params passed in constructor

            gameStateDispatcher.OnPlayerChased += HandlePlayerChased;
            gameStateDispatcher.OnPrankFound += HandlePrankFound;

            SwitchMusicTo(BackgroundMusicType.Exploration);
        }

        public void Update(float deltaTime)
        {
            if(Input.GetKey(KeyCode.Space))
            {
                Debug.Log("Playing music: " + currentMusicType);
            }

            if (currentMusicType == BackgroundMusicType.Exploration)
                return;

            cooldownTimer -= deltaTime;
            if(cooldownTimer <= 0 )
            {
                currentMusicType = (BackgroundMusicType)Mathf.Max((int)currentMusicType--, 0);
                SwitchMusicTo(currentMusicType);
            }
        }

        void HandlePlayerChased()
        {
            if(currentMusicType != BackgroundMusicType.Chase)
            {
                SwitchMusicTo(BackgroundMusicType.Chase);
            }
        }

        void HandlePrankFound()
        {
            if(currentMusicType == BackgroundMusicType.Exploration)
            {
                SwitchMusicTo(BackgroundMusicType.Searching);
            }
        }

        void SwitchMusicTo(BackgroundMusicType musicType)
        {
            Debug.Log("Switching music from: " + currentMusicType + " to: " + musicType);
            cooldownTimer = musicCooldownTime;
            currentMusicType = musicType;

            // Fade out previous music
            // Fade in new music
        }
    }
}