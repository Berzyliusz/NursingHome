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

        //TODO: We should get params passed in constructor
        const float musicCooldownTime = 5f;
        const float musicFadeTime = 2f;

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
            cooldownTimer = musicCooldownTime;
            currentMusicType = musicType;

            previousMusicIndex = currentMusicIndex;
            audioPlayer.StopSoundDelayed(previousMusicIndex, musicFadeTime);
            audioPlayer.SetSoundVolume(previousMusicIndex, 0, musicFadeTime);

            AudioClip clip = backgroundAudioCollection[(int)currentMusicType];
            currentMusicIndex = audioPlayer.PlaySound(clip, backgroundAudioCollection, Vector3.zero);
            audioPlayer.SetSoundVolume(currentMusicIndex, 0, 0.0f);
            audioPlayer.SetSoundVolume(currentMusicIndex, backgroundAudioCollection.Volume, musicFadeTime);
        }
    }
}