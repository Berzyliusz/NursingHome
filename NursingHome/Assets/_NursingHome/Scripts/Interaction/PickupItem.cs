using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class PickupItem : InteractableItem
    {
        public override string DisplayName => itemParams.ItemName;
        public int ChargesAmount => chargesAmount;

        [SerializeField]
        [Tooltip("How many charges this item has, before it uses up?")]
        uint chargesAmount = 1;

        [SerializeField]
        [InlineEditor]
        PickableItemParams itemParams;

        public override ItemParams ItemParams => itemParams;

        public override void UseItem()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}