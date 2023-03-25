using UnityEngine;

namespace NursingHome.Interactions
{
    public class UsableItem : InteractableItem
    {
        public override string DisplayName => ItemParams.ItemName;

        public override ItemParams ItemParams => itemParams;

        [SerializeField]
        UsableItemParams itemParams;

        public override void UseItem()
        {
            
        }
    }
}