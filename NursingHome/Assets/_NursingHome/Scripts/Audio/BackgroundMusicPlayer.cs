using Audio;
using UnityEngine;
using UnityEngine.UIElements;

namespace NursingHome.Audio
{
    public interface IBackgroundMusicPlayer : IUpdateable
    {
        void StartBackgroundMusic();
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

        ulong currentMusicIndex;
        ulong previousMusicIndex;

        IGameStateDispatcher gameStateDispatcher;
        readonly AudioCollection backgroundAudioCollection;
        private readonly AudioPlayer audioPlayer;
        BackgroundMusicType currentMusicType;

        public BackgroundMusicPlayer(IGameStateDispatcher gameStateDispatcher, AudioCollection backgroundAudioCollection, AudioPlayer audioPlayer)
        {
            this.gameStateDispatcher = gameStateDispatcher;
            this.backgroundAudioCollection = backgroundAudioCollection;
            this.audioPlayer = audioPlayer;

            // We should get params passed in constructor

            gameStateDispatcher.OnPlayerChased += HandlePlayerChased;
            gameStateDispatcher.OnPrankFound += HandlePrankFound;
        }

        public void StartBackgroundMusic()
        {
            SwitchMusicTo(BackgroundMusicType.Exploration);
        }

        public void Update(float deltaTime)
        {
            if (currentMusicType == BackgroundMusicType.Exploration)
                return;

            cooldownTimer -= deltaTime;
            if(cooldownTimer <= 0 )
            {
                SwitchMusicTo(BackgroundMusicType.Exploration);
            }
        }

        void HandlePlayerChased()
        {
            if(currentMusicType != BackgroundMusicType.Chase)
            {
                SwitchMusicTo(BackgroundMusicType.Chase);
            }
            else
            {
                cooldownTimer = musicCooldownTime;
            }
        }

        void HandlePrankFound()
        {
            if(currentMusicType == BackgroundMusicType.Exploration)
            {
                SwitchMusicTo(BackgroundMusicType.Searching);
            }
            else
            {
                cooldownTimer = musicCooldownTime;
            }
        }

        void SwitchMusicTo(BackgroundMusicType musicType)
        {
            Debug.Log($"Switching music from {(int)currentMusicType}: {currentMusicType} to {(int)musicType}: {musicType}");

            cooldownTimer = musicCooldownTime;
            currentMusicType = musicType;

            audioPlayer.StopSound(currentMusicIndex);
            previousMusicIndex = currentMusicIndex;
            // Actually make it stop delayed, after a fade out
            // audioPlayer.StopSoundDelayed(currentMusicIndex, fadeDuration)
            // audioPlayer.FadeTrackVolume.. 

            AudioClip clip = backgroundAudioCollection[(int)currentMusicType];
            currentMusicIndex = audioPlayer.PlaySound(clip, backgroundAudioCollection, Vector3.zero);

            // Fade out previous music
            // Fade in new music
        }
    }
}