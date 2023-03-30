using System;
using System.Collections.Generic;

namespace NursingHome.Interactions
{
    public class ItemUser : InteractionReceiverBase
    {
        public event Action<ItemParams, PrankParams> OnItemUsed;
        Dictionary<UsableElement, PrankParams> performedPranks = new Dictionary<UsableElement, PrankParams>();

        protected override void HandleInteractionDetected(InteractableElement interactedItem)
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
            // We have no idea what item held this prank capabilities.
            //Todo: Rework, so we can choose what item we want to use, as we may have many

            if (chosenPrank == null)
                return;

            if(inputs.Use)
            {
                var usedElement = Systems.Instance.InteractionDetector.GetUsableElement();

                //Todo: Rework, so we can have multiple performed pranks per item.
                if(!performedPranks.ContainsKey(usedElement))
                {
                    performedPranks[usedElement] = chosenPrank;
                    usedElement.UseElement();
                    OnItemUsed?.Invoke(usedElement.ItemParams, chosenPrank);
                }
            }
        }

        PrankParams ChoosePrankToUse()
        {
            var items = Systems.Instance.Inventory.GetAvailableItemsForElement(Systems.Instance.InteractionDetector.GetUsableElement().ItemParams);
            // TODO: Add scroll to choose item, highlight it too.

            if (items == null || items.Count == 0)
                return null;

            // We dont need to scroll and choose what action we want -> for now there is only one.
            return items[0].ItemParams.PrankParams[0];
        }
    }
}