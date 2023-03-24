using UnityEngine;

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
                Debug.Log("Hiding UI");
                return;
            }

            if(item.CompareTag(pickupTag))
            {
                Debug.Log("Displaying pickup UI!");
            }
        }
    }
}