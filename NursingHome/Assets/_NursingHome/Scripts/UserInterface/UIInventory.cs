using NursingHome.UserInterface;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public class UIInventory : UIElement
    {
        public override UIType Type => UIType.Inventory;

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