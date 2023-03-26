using System;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class ItemUser : MonoBehaviour
    {
        public event Action<ItemParams, PrankParams> OnItemUsed;

        void Start()
        {
            InteractionDetector.OnInteractionDetected += HandleInteractionDetected;
            enabled = false;
        }

        void OnDestroy()
        {
            InteractionDetector.OnInteractionDetected -= HandleInteractionDetected;
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

            if(Systems.Instance.Inputs.Use)
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