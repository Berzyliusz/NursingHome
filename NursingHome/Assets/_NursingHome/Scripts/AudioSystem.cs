using Audio;

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

        public AudioSystem(AudioPlayer player, IGameStateDispatcher gameStateDispatcher)
        {
            this.player = player;

            BackgroundPlayer = new BackgroundMusicPlayer(gameStateDispatcher);
            // We need to pass here an audio collection with background music
        }

        public void Update(float deltaTime)
        {
            BackgroundPlayer.Update(deltaTime);
        }
    }
}