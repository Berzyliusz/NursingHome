namespace NursingHome.UserInterface
{
    public class UIUsables : UIElement
    {
        public override UIType Type => UIType.Use;

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
            // Just display given actions in given order
        }
    }
}