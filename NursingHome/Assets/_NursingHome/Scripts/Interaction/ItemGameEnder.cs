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
            var systems = Systems.Instance;
            systems.Time.SetTimeScale(0.0f);
            systems.Player.SetFreezePlayer(true);
            systems.UISystem.HideScreen(UIType.AimDot);
            systems.Cursor.SetCursorVisible(true);
            systems.Cursor.SetCursorLocked(CursorLockMode.None);
            systems.UISystem.HideScreen(UIType.GameEndPrompt);

            systems.UISystem.ShowScreen(UIType.WinScreen);

            // What do we need to pass to the UI?
            // We need prank names
            // and their point values
            // for now we can pass it as a set of strings?
            // update WinUI
        }
    }
}
