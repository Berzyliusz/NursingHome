using UnityEngine;

namespace NursingHome
{
    public interface ITime
    {
        void SetTimeScale(float timeScale);
    }

    public class TimeHandler : ITime
    {
        public void SetTimeScale(float timeScale)
        {
            Time.timeScale = timeScale;
        }
    }
}