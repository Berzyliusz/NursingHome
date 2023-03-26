using UnityEngine;

namespace NursingHome.Interactions
{
    public class InteractionReceiverBase : MonoBehaviour
    {
        protected InteractionDetector interactionDetector;
        protected IInputs inputs;

        protected virtual void Start()
        {
            interactionDetector = Systems.Instance.InteractionDetector;
            inputs = Systems.Instance.Inputs;
            interactionDetector.OnInteracted += HandleInteractionDetected;
            enabled = false;
        }

        protected virtual void HandleInteractionDetected(InteractableItem interactedItem)
        {

        }
    }
}