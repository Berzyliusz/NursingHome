using System;

namespace NursingHome.Interactions
{

    public class ItemPicker : InteractionReceiverBase
    {
        public event Action<ItemParams> OnItemPicked;

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
                var item = interactionDetector.SelectedItem;
                item.UseItem();
                OnItemPicked?.Invoke(item.ItemParams);
                // We pass just the params... the amount of charges is kinda lost
                // We can pack it into small class, containing ALL parameters. 
                // And forget the visual representation and mono
            }
        }
    }
}