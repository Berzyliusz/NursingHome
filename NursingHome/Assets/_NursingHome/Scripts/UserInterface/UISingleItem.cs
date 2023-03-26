using UnityEngine;
using TMPro;

namespace NursingHome.UserInterface
{
    public class UISingleItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI itemNameText;

        public void SetItemText(string text)
        {
            itemNameText.text = text;
        }
    }
}