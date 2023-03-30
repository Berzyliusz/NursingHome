using System;

namespace NursingHome.Interactions
{

    public class ItemPicker : InteractionReceiverBase
    {
        public event Action<Item> OnItemPicked;

        protected override void HandleInteractionDetected(InteractableItem interactedItem)
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
                var pickupItem = interactionDetector.GetPickupItem();

                if (pickupItem == null)
                    return;

                pickupItem.UseItem();
                OnItemPicked?.Invoke(pickupItem.Item);
            }
        }
    }
}