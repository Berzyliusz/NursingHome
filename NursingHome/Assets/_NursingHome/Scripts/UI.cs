using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public enum UIType
    {
        WinScreen,
        LooseScreen,
        PickupPrompt
    }

    public class UI : MonoBehaviour
    {
        [SerializeField]
        UIElement[] uIElements;

        Dictionary<UIType, UIElement> uIElementsDict;

        void Awake()
        {
            foreach(var element in uIElements)
            {
                uIElementsDict[element.Type] = element;
            }
        }

        public void HideScreen(UIType typeToHide)
        {

        }

        public void ShowScreen(UIType typeToShow)
        {

        }

        public void UpdateScreen(UIType typeToUpdate)
        {

        }
    }
}