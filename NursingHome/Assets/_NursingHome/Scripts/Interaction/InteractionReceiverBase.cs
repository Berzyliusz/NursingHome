using UnityEngine;

namespace NursingHome.Interactions
{
    public class InteractionReceiverBase : MonoBehaviour
    {
        protected IInteractionDetector interactionDetector;
        protected IInputs inputs;

        protected virtual void Start()
        {
            interactionDetector = Systems.Instance.interactionDetector;
            inputs = Systems.Instance.Inputs;
            interactionDetector.OnInteracted += HandleInteractionDetected;
            enabled = false;
        }

        protected virtual void HandleInteractionDetected(InteractableElement interactedItem)
        {

        }
    }
}