using UnityEngine;
using NursingHome.UserInterface;

namespace NursingHome.Interactions
{
    public class InteractionUIDisplayer : MonoBehaviour
    {
        UIParams uiParams;

        void Awake()
        {
            InteractionDetector.OnInteractionDetected += HandleInteractionDetected;
        }

        void OnDestroy()
        {
            InteractionDetector.OnInteractionDetected -= HandleInteractionDetected;
        }

        void HandleInteractionDetected(InteractableElement item)
        {
            if(item != null)
            {
                DisplayInteractableUI(item);
            }
            else
            {
                Systems.Instance.UISystem.HideScreen(UIType.PickupPrompt);
                Systems.Instance.UISystem.HideScreen(UIType.Use);
            }
        }

        void DisplayInteractableUI(InteractableElement item)
        {
            HandlePickableItem(item);
            HandleUsableItem(item);
        }

        void HandlePickableItem(InteractableElement item)
        {
            if (item.CompareTag(Tags.Pickable))
            {
                uiParams.Name = item.DisplayName;
                Systems.Instance.UISystem.ShowScreen(UIType.PickupPrompt);
                Systems.Instance.UISystem.UpdateScreen(UIType.PickupPrompt, uiParams);
            }
            else
            {
                Systems.Instance.UISystem.HideScreen(UIType.PickupPrompt);
            }
        }

        void HandleUsableItem(InteractableElement item)
        {
            if (item.CompareTag(Tags.Usable))
            {
                var availablePranks = Systems.Instance.Inventory.GetAvailableItemsForElement(item.ItemParams);

                uiParams.Names = new string[availablePranks.Count];
                for (int i = 0; i < availablePranks.Count; i++)
                {
                    uiParams.Names[i] = string.Format($"{i + 1}. {availablePranks[i].ItemParams.PrankParams[0].DisplayName}");
                }

                Systems.Instance.UISystem.ShowScreen(UIType.Use);
                Systems.Instance.UISystem.UpdateScreen(UIType.Use, uiParams);
            }
            else
            {
                Systems.Instance.UISystem.HideScreen(UIType.Use);
            }
        }
    }
}