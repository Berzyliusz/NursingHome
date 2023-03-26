using System.Collections;

namespace NursingHome.UserInterface
{
    public class UIWinScreen : PoolingUIElement
    {
        public override UIType Type => UIType.WinScreen;

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
            DestroySpawnedItems();
        }
    }
}