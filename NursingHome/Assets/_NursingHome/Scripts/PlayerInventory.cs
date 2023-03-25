using UnityEngine;
using NursingHome.Interactions;
using System;
using System.Collections.Generic;

namespace NursingHome
{
    public class PlayerInventory
    {
        public event Action<ItemParams> OnItemAdded;

        public List<ItemParams> Items { get; private set; } = new List<ItemParams>();

        public PlayerInventory(ItemPicker picker)
        {
            picker.OnItemPicked += HandleItemPicked;
        }

        void HandleItemPicked(ItemParams pickedItem)
        {
            OnItemAdded?.Invoke(pickedItem);
            Items.Add(pickedItem);
        }
    }
}