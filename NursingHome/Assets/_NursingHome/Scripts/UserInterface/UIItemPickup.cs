using TMPro;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public class UIItemPickup : UIElement
    {
        [SerializeField]
        TextMeshProUGUI itemNameText;

        public override UIType Type => UIType.PickupPrompt;

        public override void Hide()
        {
            parent.SetActive(false);
        }

        public override void Show()
        {
            parent.SetActive(true);
        }

        public override void UpdateUI(UIParams uiParams)
        {
            itemNameText.text = uiParams.Name;
        }
    }
}