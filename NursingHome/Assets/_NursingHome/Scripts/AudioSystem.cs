using Audio;

namespace NursingHome.Audio
{
    public interface IBackgroundMusicPlayer
    {

    }

    public interface IAudioSystem
    {
        IBackgroundMusicPlayer BackgroundPlayer { get; }
    }

    public class AudioSystem : IAudioSystem
    {
        public IBackgroundMusicPlayer BackgroundPlayer => throw new System.NotImplementedException();

        readonly AudioPlayer player;

        public AudioSystem(AudioPlayer player)
        {
            this.player = player;
        }

    }
}