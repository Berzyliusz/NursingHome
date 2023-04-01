using UnityEngine;

namespace NursingHome.AI
{
    public interface IEmotionDisplayer
    {
        EmotionType CurrentEmotion { get; }
        void SetEmotion(EmotionType type);
    }

    public enum EmotionType
    {
        None = 0,
        Curious = 1,
        Angry = 2
    };

    public class AiEmotionDisplayer : IEmotionDisplayer
    {
        public EmotionType CurrentEmotion => throw new System.NotImplementedException();

        readonly Transform emotionIconTransform;
        private readonly EmotionWithType[] emotions;

        public AiEmotionDisplayer(Transform emotionIconTransform, EmotionWithType[] emotions)
        {
            this.emotionIconTransform = emotionIconTransform;
            this.emotions = emotions;
        }


        public void SetEmotion(EmotionType type)
        {
            throw new System.NotImplementedException();
        }
    }
}