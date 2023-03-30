using UnityEngine;
using TMPro;

namespace NursingHome.UserInterface
{
    public class UISingleItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI itemNameText;

        [SerializeField]
        TextMeshProUGUI subText;

        public void SetItemText(string text)
        {
            itemNameText.text = text;
        }

        public void SetSubtext(string text)
        {
            subText.text = text;
        }
    }
}