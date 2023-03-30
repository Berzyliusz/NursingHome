using NursingHome.Interactions;
using System;
using System.Collections.Generic;
using NursingHome.UserInterface;

namespace NursingHome
{
    public class PlayerInventory
    {
        public event Action<Item> OnItemAdded;

        public List<Item> Items { get; private set; } = new List<Item>();
        Dictionary<PrankParams, Item> itemsByPranks = new Dictionary<PrankParams, Item>();
        UIParams uiParams;

        public PlayerInventory(ItemPicker picker)
        {
            picker.OnItemPicked += HandleItemPicked;
        }

        public List<Item> GetAvailableItemsForElement(ItemParams usedElement)
        {
            List<Item> result = new List<Item>();

            foreach(var prank in usedElement.PrankParams)
            {
                if(itemsByPranks.ContainsKey(prank))
                {
                    if (itemsByPranks[prank].ChargesAmount > 0)
                    {
                        result.Add(itemsByPranks[prank]);
                    }
                }
            }

            return result;
        }

        void HandleItemPicked(Item pickedItem)
        {
            OnItemAdded?.Invoke(pickedItem);
            Items.Add(pickedItem);

            foreach(var prank in pickedItem.ItemParams.PrankParams)
            {
                itemsByPranks[prank] = pickedItem;
            }

            Systems.Instance.UISystem.ShowScreen(UIType.Inventory);
            uiParams.Names = new string[Items.Count];
            for(int i = 0; i < Items.Count; i++)
            {
                uiParams.Names[i] = Items[i].ItemParams.ItemName;
            }

            Systems.Instance.UISystem.UpdateScreen(UIType.Inventory, uiParams);
        }
    }
}