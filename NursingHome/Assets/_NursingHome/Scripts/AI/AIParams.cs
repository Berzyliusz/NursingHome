using UnityEngine;

namespace NursingHome.AI
{
    [CreateAssetMenu(menuName = "NursingHome /AI Params")]
    public class AIParams : ScriptableObject
    {
        [field: SerializeField]
        public float WalkSpeed { get; private set; } = 3.2f;
        [field: SerializeField]
        public float ChaseSpeed { get; private set; } = 5.0f;
        [field: SerializeField]
        public float InRangeDistance { get; private set; } = 0.2f;
    }
}