using System;
using System.Collections.Generic;

namespace NursingHome.Interactions
{
    public class ItemUser : InteractionReceiverBase
    {
        /// <summary>
        /// Invoked when a prank is being performed, passing in the element it was used upon, the item used, and the prank itself.
        /// </summary>
        public event Action<UsableElement, Item, PrankParams> OnItemUsed;
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
            var chosenItem = ChooseItemToUse();
            // We have no idea what item held this prank capabilities.

            if (chosenItem == null)
                return;

            if(inputs.Use)
            {
                var usedElement = Systems.Instance.InteractionDetector.GetUsableElement();

                //Todo: Rework, so we can have multiple performed pranks per item.
                if(!performedPranks.ContainsKey(usedElement))
                {
                    // We are performing a prank. 
                    // We need to know what item was it.
                    // And use one charge of that item.

                    performedPranks[usedElement] = chosenItem.ItemParams.PrankParams[0];
                    usedElement.UseElement();
                    OnItemUsed?.Invoke(usedElement, chosenItem, chosenItem.ItemParams.PrankParams[0]);
                }
            }
        }

        Item ChooseItemToUse()
        {
            var items = Systems.Instance.Inventory.GetAvailableItemsForElement(Systems.Instance.InteractionDetector.GetUsableElement().ItemParams);
            // TODO: Add scroll to choose item, highlight it too.

            if (items == null || items.Count == 0)
                return null;

            // We dont need to scroll and choose what action we want -> for now there is only one.
            return items[0];
        }
    }
}