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
            if(item == null || !item.CompareTag(pickupTag))
            {
                Systems.Instance.UISystem.HideScreen(UIType.PickupPrompt);
            }

            //TODO: solve the bails, for null, but also handle hiding UI screens.

            if (item == null || !item.CompareTag(interactableTag))
            {
                Systems.Instance.UISystem.HideScreen(UIType.Use);
            }

            if (item.CompareTag(pickupTag))
            {
                uiParams.Name = item.DisplayName;
                Systems.Instance.UISystem.ShowScreen(UIType.PickupPrompt);
                Systems.Instance.UISystem.UpdateScreen(UIType.PickupPrompt, uiParams);
            }

            if(item.CompareTag(interactableTag))
            {
                Debug.Log($"Interactable item detected {item.DisplayName}");
            }
        }
    }
}