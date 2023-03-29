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
        BackgroundMusicType musicType;

        public BackgroundMusicPlayer(IGameStateDispatcher gameStateDispatcher)
        {
            this.gameStateDispatcher = gameStateDispatcher;

            // We should get params passed in constructor

            gameStateDispatcher.OnPlayerChased += HandlePlayerChased;
            gameStateDispatcher.OnPrankFound += HandlePrankFound;

            musicType = BackgroundMusicType.Exploration;
        }

        public void Update(float deltaTime)
        {
            cooldownTimer -= deltaTime;

            // if cooldown timer below zero, go lower
        }

        void HandlePlayerChased()
        {
            if(musicType != BackgroundMusicType.Chase)
            {
                musicType = BackgroundMusicType.Chase;

                SwitchMusicTo(musicType);
            }
        }

        void HandlePrankFound()
        {
            if(musicType != BackgroundMusicType.Searching)
            {
                musicType = BackgroundMusicType.Searching;

                SwitchMusicTo(musicType);

            }
        }

        void SwitchMusicTo(BackgroundMusicType musicType)
        {
            cooldownTimer = musicCooldownTime;

            // Fade out previous music
            // Fade in new music
        }
    }
}