namespace NursingHome.UserInterface
{
    public class UIGameEndPrompt : UIElement
    {
        public override UIType Type => UIType.GameEndPrompt;

        public override void Hide()
        {
            parent.gameObject.SetActive(false);
        }

        public override void Show()
        {
            parent.gameObject.SetActive(true);
        }

        public override void UpdateUI(UIParams uiParams)
        {
            
        }
    }
}