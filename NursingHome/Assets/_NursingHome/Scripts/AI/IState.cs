namespace NursingHome.AI
{
    public interface IState
    {
        void StartState();
        void EndState();
        void UpdateState();
        bool IsStateDone();
    }
}