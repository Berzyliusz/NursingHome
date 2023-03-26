using System;
using UnityEngine;
using UnityEngine.Windows;

namespace NursingHome.Interactions
{
    public class ItemUser : MonoBehaviour
    {
        public event Action<ItemParams, PrankParams> OnItemUsed;

        InteractionDetector interactionDetector;
        IInputs inputs;

        void Start()
        {
            interactionDetector = Systems.Instance.InteractionDetector;
            inputs = Systems.Instance.Inputs;
            interactionDetector.OnInteracted += HandleInteractionDetected;
            enabled = false;
        }

        void HandleInteractionDetected(InteractableItem interactedItem)
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
                OnItemUsed?.Invoke(interactedItem.ItemParams, chosenPrank);
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