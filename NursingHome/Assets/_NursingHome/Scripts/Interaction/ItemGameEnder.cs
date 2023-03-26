using NursingHome.Interactions;
using UnityEngine;

public class ItemGameEnder : InteractionReceiverBase
{
    protected override void HandleInteractionDetected(InteractableItem interactedItem)
    {
        if (interactedItem == null || !interactedItem.gameObject.CompareTag(Tags.Respawn))
        {
            enabled = false;
        }
        else
        {
            Debug.LogError("Game end detected");
            enabled = true;
        }

    }
}
