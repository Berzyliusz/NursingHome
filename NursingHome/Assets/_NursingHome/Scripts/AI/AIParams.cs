using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.AI
{
    [System.Serializable]
    public struct EmotionWithType
    {
        [field: SerializeField]
        public EmotionType Type { get; private set; }
        [field: SerializeField]
        [AssetsOnly]
        public GameObject Icon { get; private set; }
    }

    [CreateAssetMenu(menuName = "NursingHome /AI Params")]
    public class AIParams : ScriptableObject
    {
        [FoldoutGroup("Movement")]
        [field: SerializeField]
        public float WalkSpeed { get; private set; } = 3.2f;
        [FoldoutGroup("Movement")]
        [field: SerializeField]
        public float ChaseSpeed { get; private set; } = 5.0f;
        [FoldoutGroup("Movement")]
        [field: SerializeField]
        public float InRangeDistance { get; private set; } = 0.2f;

        [FoldoutGroup("Eyesight")]
        [field: SerializeField]
        public float InFrontAngle { get; private set; } = 30.0f;
        [FoldoutGroup("Eyesight")]
        [field: SerializeField] 
        public float EyesightRange { get; private set; } = 30.0f;
        [FoldoutGroup("Eyesight")]
        [field: SerializeField]
        public LayerMask EyesLayerMask { get; private set; }

        [field: SerializeField]
        [FoldoutGroup("Emotions")]
        public EmotionWithType[] Emotions { get; private set; } = new EmotionWithType[0];
    }
}