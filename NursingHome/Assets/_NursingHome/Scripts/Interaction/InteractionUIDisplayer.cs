using UnityEngine;
using NursingHome.UserInterface;

namespace NursingHome.Interactions
{
    public class InteractionUIDisplayer : MonoBehaviour
    {
        const string pickupTag = "Pickup";
        const string interactableTag = "Interactable";

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
                Systems.Instance.UISystem.HideScreen(UIType.PickupPrompt);
            }
        }

        private void DisplayInteractableUI(InteractableItem item)
        {
            if (item.CompareTag(pickupTag))
            {
                uiParams.Name = item.DisplayName;
                Systems.Instance.UISystem.ShowScreen(UIType.PickupPrompt);
                Systems.Instance.UISystem.UpdateScreen(UIType.PickupPrompt, uiParams);
            }
            else
            {
                Systems.Instance.UISystem.HideScreen(UIType.PickupPrompt);
            }

            if (item.CompareTag(interactableTag))
            {
                Debug.Log($"Interactable item detected {item.DisplayName}");
            }
            else
            {
                Systems.Instance.UISystem.HideScreen(UIType.Use);
            }
        }
    }
}