using System;

namespace NursingHome
{
    public interface IGameStateDispatcher
    {
        void DisptachPrankFound();
        void DispatchPlayerChased();

        event Action OnPrankFound;
        event Action OnPlayerChased;
    }

    public class GameStateDispatcher : IGameStateDispatcher
    {
        public event Action OnPrankFound;
        public event Action OnPlayerChased;

        public void DispatchPlayerChased()
        {
            OnPlayerChased?.Invoke();
        }

        public void DisptachPrankFound()
        {
            OnPrankFound?.Invoke();
        }
    }
}