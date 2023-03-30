using UnityEngine;

namespace NursingHome.Interactions
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractableElement : MonoBehaviour
    {
        public abstract string DisplayName { get; }
        public abstract ItemParams ItemParams { get; }
        public abstract void UseElement();
    }
}