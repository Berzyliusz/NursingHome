using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public class UIAimDot : UIElement
    {
        public override UIType Type => UIType.AimDot;

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