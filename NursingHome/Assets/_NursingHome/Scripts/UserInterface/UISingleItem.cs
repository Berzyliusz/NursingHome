using UnityEngine;
using TMPro;

namespace NursingHome.UserInterface
{
    public class UISingleItem : MonoBehaviour
    {
        [SerializeField]
        TextMeshProUGUI itemNameText;

        public void SetItemName(string itemName)
        {
            itemNameText.text = itemName;
        }
    }
}