namespace NursingHome.UserInterface
{
    public class UILooseScreen : UIElement
    {
        public override UIType Type => UIType.LooseScreen;

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