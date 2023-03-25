using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public enum UIType
    {
        WinScreen,
        LooseScreen,
        PickupPrompt,
        Inventory,
        Use
    }

    public struct UIParams //TODO: Make it more generic or derived
    {
        public string Name;
        public string[] Names;
    }

    public class UISystem : MonoBehaviour
    {
        [SerializeField]
        UIElement[] uIElements;

        Dictionary<UIType, UIElement> uIElementsDict = new Dictionary<UIType, UIElement>();

        void Awake()
        {
            foreach(var element in uIElements)
            {
                uIElementsDict[element.Type] = element;
                element.Hide();
            }
        }

        public void HideScreen(UIType typeToHide)
        {
            if(uIElementsDict.TryGetValue(typeToHide, out var element))
            {
                element.Hide();
            }
        }

        public void ShowScreen(UIType typeToShow)
        {
            if(uIElementsDict.TryGetValue(typeToShow, out var element))
            {
                element.Show();
            }
        }

        public void UpdateScreen(UIType typeToUpdate, UIParams uiParams)
        {
            if (uIElementsDict.TryGetValue(typeToUpdate, out var element))
            {
                element.UpdateUI(uiParams);
            }
        }
    }
}