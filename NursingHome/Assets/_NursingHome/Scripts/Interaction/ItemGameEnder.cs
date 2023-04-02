using NursingHome;
using NursingHome.Interactions;
using UnityEngine;
using NursingHome.UserInterface;

public class ItemGameEnder : InteractionReceiverBase
{
    protected override void HandleInteractionDetected(InteractableElement interactedItem)
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
        if (!inputs.Use)
            return;

        WinGame();

        //TODO: Also be able to handle game lost
    }

    void WinGame()
    {
        var systems = Systems.Instance;
        systems.Time.SetTimeScale(0.0f);
        systems.Player.SetFreezePlayer(true);
        systems.UISystem.HideScreen(UIType.AimDot);
        systems.Cursor.SetCursorVisible(true);
        systems.Cursor.SetCursorLocked(CursorLockMode.None);
        systems.UISystem.HideScreen(UIType.GameEndPrompt);

        systems.UISystem.ShowScreen(UIType.WinScreen);

        var score = systems.Score;
        var pranks = score.GetPranks();

        UIParams winParams = new();
        winParams.Name = $"{score.GetStartingScore()} starting points + {score.GetScoreEarnedForDay()} points = {score.GetTotalScore()} total points";

        winParams.Names = new string[pranks.Count];

        int i = 0;
        foreach (var pair in pranks)
        {
            var points = pair.Key.PrankPoints;
            var amount = pair.Value;
            //Todo: Decide what to display according to design
            string entry = (i + 1).ToString() + ". " + pair.Key.DisplayName + ".  " + points + " x " + amount + " = " + (points * amount).ToString();
            winParams.Names[i] = entry;
            i++;
        }

        systems.UISystem.UpdateScreen(UIType.WinScreen, winParams);
        systems.interactionDetector.SetCanDetectInteraction(false);
    }
}
