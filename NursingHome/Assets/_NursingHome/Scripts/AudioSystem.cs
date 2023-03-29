using Audio;
using UnityEngine;

namespace NursingHome.Audio
{
    public interface IAudioSystem : IUpdateable
    {
        IBackgroundMusicPlayer BackgroundPlayer { get; }
        // Expose common methods like Play, Pause, Stop etc.
    }

    public class AudioSystem : IAudioSystem
    {
        public IBackgroundMusicPlayer BackgroundPlayer { get; private set; }

        readonly AudioPlayer player;

        public AudioSystem(AudioPlayer player, IGameStateDispatcher gameStateDispatcher, AudioCollection backgroundAudioCollection)
        {
            this.player = player;

            BackgroundPlayer = new BackgroundMusicPlayer(gameStateDispatcher, backgroundAudioCollection, player);
        }

        public void Update(float deltaTime)
        {
            BackgroundPlayer.Update(deltaTime);
        }
    }
}