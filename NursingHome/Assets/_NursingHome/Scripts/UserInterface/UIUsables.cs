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
            // Display this items "normal" usages if it has any 

            // Check player inventory to see if we can add more actions 

            // Display them if necessary
        }
    }
}