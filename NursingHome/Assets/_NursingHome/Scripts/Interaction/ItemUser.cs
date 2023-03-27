using System;
using System.Collections.Generic;

namespace NursingHome.Interactions
{
    public class ItemUser : InteractionReceiverBase
    {
        public event Action<ItemParams, PrankParams> OnItemUsed;
        Dictionary<InteractableItem, PrankParams> performedPranks = new Dictionary<InteractableItem, PrankParams>();

        protected override void HandleInteractionDetected(InteractableItem interactedItem)
        {
            if(interactedItem == null || !interactedItem.gameObject.CompareTag(Tags.Usable))
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
            var chosenPrank = ChoosePrankToUse();

            if (chosenPrank == null)
                return;

            if(inputs.Use)
            {
                var interactedItem = Systems.Instance.InteractionDetector.SelectedItem;

                //Todo: Rework, so we can have multiple performed pranks per item.
                if(!performedPranks.ContainsKey(interactedItem))
                {
                    performedPranks[interactedItem] = chosenPrank;
                    OnItemUsed?.Invoke(interactedItem.ItemParams, chosenPrank);
                }
            }
        }

        PrankParams ChoosePrankToUse()
        {
            var pranks = Systems.Instance.Inventory.GetAvailablePranksForItem(Systems.Instance.InteractionDetector.SelectedItem.ItemParams);
            // TODO: Add scroll to choose item, highlight it too.

            if (pranks == null || pranks.Count == 0)
                return null;

            // We dont need to scroll and choose what action we want -> for now there is only one.
            return pranks[0];
        }
    }
}