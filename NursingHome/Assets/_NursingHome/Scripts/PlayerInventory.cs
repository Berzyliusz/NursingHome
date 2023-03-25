using UnityEngine;
using NursingHome.Interactions;

namespace NursingHome
{
    public class PlayerInventory
    {
        public PlayerInventory(ItemPicker picker)
        {
            picker.OnItemPicked += HandleItemPicked;
        }

        void HandleItemPicked(ItemParams pickedItem)
        {
            Debug.Log($"Inventory received item: {pickedItem.name}");
        }
    }
}