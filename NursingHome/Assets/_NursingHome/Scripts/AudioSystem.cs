using Audio;

namespace NursingHome.Audio
{
    public interface IAudioSystem
    {
        IBackgroundMusicPlayer BackgroundPlayer { get; }
    }

    public class AudioSystem : IAudioSystem
    {
        public IBackgroundMusicPlayer BackgroundPlayer { get; private set; }

        readonly AudioPlayer player;

        public AudioSystem(AudioPlayer player)
        {
            this.player = player;

            BackgroundPlayer = new BackgroundMusicPlayer();
            // We need to pass here an audio collection with background music
        }

    }
}