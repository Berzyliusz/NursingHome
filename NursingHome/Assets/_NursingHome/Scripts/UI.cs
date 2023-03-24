namespace NursingHome
{
    public interface IUI
    {
        void ShowScreen(UIType typeToShow);
        void HideScreen(UIType typeToHide);
    }

    public enum UIType
    {
        WinScreen,
        LooseScreen,
        PickupPrompt
    }

    public class UI : IUI
    {
        public UI()
        {

        }

        public void HideScreen(UIType typeToHide)
        {

        }

        public void ShowScreen(UIType typeToShow)
        {

        }
    }
}