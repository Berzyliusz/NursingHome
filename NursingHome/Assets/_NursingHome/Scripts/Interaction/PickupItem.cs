using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class PickupItem : InteractableItem
    {
        public override string DisplayName => itemParams.ItemName; // Remove, use item instead
        public Item Item => item;

        [SerializeField]
        [Tooltip("How many charges this item has, before it uses up?")]
        uint chargesAmount = 1;

        [SerializeField]
        [InlineEditor]
        PickableItemParams itemParams;

        Item item;

        public override ItemParams ItemParams => itemParams; // Make this private, use item instead

        void Awake()
        {
            item = new Item(chargesAmount, itemParams);
        }

        public override void UseItem()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}