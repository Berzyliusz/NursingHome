using System;
using UnityEngine;

namespace NursingHome.Audio
{
    public interface IBackgroundMusicPlayer
    {

    }

    public class BackgroundMusicPlayer : IBackgroundMusicPlayer
    {
        // Listen to events of things happening, and boost the music 

        // if nothing happened for a while, return to normal music
        private IGameStateDispatcher gameStateDispatcher;

        public BackgroundMusicPlayer(IGameStateDispatcher gameStateDispatcher)
        {
            this.gameStateDispatcher = gameStateDispatcher;

            gameStateDispatcher.OnPlayerChased += HandlePlayerChased;
            gameStateDispatcher.OnPrankFound += HandlePrankFound;
        }

        void HandlePlayerChased()
        {
            Debug.Log("Player chase received");
        }

        void HandlePrankFound()
        {
            Debug.Log("Prank found received");
        }
    }
}