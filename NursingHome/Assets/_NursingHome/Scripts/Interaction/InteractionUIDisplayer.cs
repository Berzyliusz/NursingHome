using UnityEngine;
using NursingHome.UserInterface;

namespace NursingHome.Interactions
{
    public class InteractionUIDisplayer : MonoBehaviour
    {
        const string pickupTag = "Pickup";

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
            if(item == null)
            {
                Systems.Instance.UISystem.HideScreen(UIType.PickupPrompt);
                return;
            }

            if(item.CompareTag(pickupTag))
            {
                Systems.Instance.UISystem.ShowScreen(UIType.PickupPrompt);

            }
        }
    }
}