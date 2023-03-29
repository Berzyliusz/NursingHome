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

            // Fade out previous music
            // Fade in new music
        }
    }
}