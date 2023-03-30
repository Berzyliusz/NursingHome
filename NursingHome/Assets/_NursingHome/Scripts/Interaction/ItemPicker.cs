using System;

namespace NursingHome.Interactions
{

    public class ItemPicker : InteractionReceiverBase
    {
        public event Action<Item> OnItemPicked;

        protected override void HandleInteractionDetected(InteractableElement interactedItem)
        {
            if (interactedItem == null || !interactedItem.gameObject.CompareTag(Tags.Pickable))
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
        }

        void Update()
        {
            if (inputs.Use)
            {
                var pickup = interactionDetector.GetPickupElement();

                if (pickup == null)
                    return;

                var item = pickup.Item;

                pickup.UseElement();
                OnItemPicked?.Invoke(pickup.Item);
            }
        }
    }
}