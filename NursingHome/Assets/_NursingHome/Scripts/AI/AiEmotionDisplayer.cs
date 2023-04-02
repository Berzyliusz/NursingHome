using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.AI
{
    public interface IEmotionDisplayer : IUpdateable
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
        public EmotionType CurrentEmotion { get; private set; }

        readonly Transform emotionIconTransform;
        Dictionary<EmotionType, GameObject> iconsByEmotions = new Dictionary<EmotionType, GameObject>();

        Transform currentEmotionIcon;

        public AiEmotionDisplayer(Transform emotionIconTransform, EmotionWithType[] emotions)
        {
            this.emotionIconTransform = emotionIconTransform;
            
            foreach(var emotion in emotions)
            {
                iconsByEmotions.Add(emotion.Type, emotion.Icon);
            }
        }


        public void SetEmotion(EmotionType newEmotion)
        {
            if (newEmotion == CurrentEmotion)
                return;

            //TODO: Pool emotions gameobjects

            if(currentEmotionIcon)
                GameObject.Destroy(currentEmotionIcon.gameObject);

            CurrentEmotion = newEmotion;

            if (newEmotion == EmotionType.None)
                return;

            var instancedIcon = GameObject.Instantiate(iconsByEmotions[newEmotion]);
            currentEmotionIcon = instancedIcon.transform;
            currentEmotionIcon.parent = emotionIconTransform;
            currentEmotionIcon.localPosition = Vector3.zero;
            currentEmotionIcon.localScale = Vector3.one;
        }

        public void Update(float deltaTime)
        {
            if (!currentEmotionIcon)
                return;

            //TODO: Get the player injected
            var playerPos = Systems.Instance.Player.GetPlayerPosition();
            var lookAtPos = new Vector3(playerPos.x, currentEmotionIcon.position.y, playerPos.z);

            currentEmotionIcon.LookAt(lookAtPos);
        }
    }
}