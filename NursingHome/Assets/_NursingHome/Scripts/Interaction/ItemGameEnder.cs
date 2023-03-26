using NursingHome;
using NursingHome.Interactions;
using UnityEngine;
using NursingHome.UserInterface;

public class ItemGameEnder : InteractionReceiverBase
{
    protected override void HandleInteractionDetected(InteractableItem interactedItem)
    {
        if (interactedItem == null || !interactedItem.gameObject.CompareTag(Tags.Respawn))
        {
            Systems.Instance.UISystem.HideScreen(UIType.GameEndPrompt);
            enabled = false;
        }
        else
        {
            Systems.Instance.UISystem.ShowScreen(UIType.GameEndPrompt);
            enabled = true;
        }
    }

    void Update()
    {
        if(inputs.Use)
        {
            Debug.LogError("Game ending");
            Systems.Instance.Time.SetTimeScale(0.0f);
            Systems.Instance.Player.SetFreezePlayer(true);
            Systems.Instance.UISystem.HideScreen(UIType.AimDot);
            Systems.Instance.Cursor.SetCursorVisible(true);
            Systems.Instance.Cursor.SetCursorLocked(CursorLockMode.None);

            // display WinUI
        }
    }
}
