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

        void HandleInteractionDetected(InteractableItem item)
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

        void DisplayInteractableUI(InteractableItem item)
        {
            HandlePickableItem(item);
            HandleUsableItem(item);
        }

        void HandlePickableItem(InteractableItem item)
        {
            if (item.CompareTag(Tags.PickupTag))
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

        void HandleUsableItem(InteractableItem item)
        {
            if (item.CompareTag(Tags.InteractableTag))
            {
                var availablePranks = Systems.Instance.Inventory.GetAvailablePranksForItem(item.ItemParams);

                uiParams.Names = new string[availablePranks.Count];
                for (int i = 0; i < availablePranks.Count; i++)
                {
                    uiParams.Names[i] = string.Format($"{i.ToString()}. {availablePranks[i].DisplayName}");
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