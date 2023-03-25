using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class PickupItem : InteractableItem
    {
        public override string DisplayName => itemParams.ItemName;

        [SerializeField]
        [InlineEditor]
        ItemParams itemParams;

        public override ItemParams ItemParams => itemParams;

        public override void UseItem()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}