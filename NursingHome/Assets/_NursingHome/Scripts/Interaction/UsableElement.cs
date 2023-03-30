using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace NursingHome.Interactions
{
    public class UsableElement : InteractableElement
    {
        public override string DisplayName => ItemParams.ItemName;

        public override ItemParams ItemParams => itemParams;

        [field: SerializeField]
        public Transform Waypoint { get; private set; }

        [SerializeField]
        UnityEvent onItemUsed; 

        [SerializeField]
        [InlineEditor]
        UsableItemParams itemParams;

        public override void UseElement()
        {
            onItemUsed?.Invoke();
        }
    }
}