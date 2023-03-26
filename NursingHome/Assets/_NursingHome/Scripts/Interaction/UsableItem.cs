using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class UsableItem : InteractableItem
    {
        public override string DisplayName => ItemParams.ItemName;

        public override ItemParams ItemParams => itemParams;

        [field: SerializeField]
        public Transform Waypoint { get; private set; }

        [SerializeField]
        [InlineEditor]
        UsableItemParams itemParams;

        public override void UseItem()
        {
            
        }
    }
}