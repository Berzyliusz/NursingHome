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

        public PlayerInventory(ItemPicker picker, ItemUser user)
        {
            picker.OnItemPicked += HandleItemPicked;
            user.OnItemUsed += HandleItemUsed;
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

            DisplayItemsInUI(pickedItem);
        }

        void HandleItemUsed(UsableElement prankedElement, Item usedItem, PrankParams prank)
        {
            DisplayItemsInUI(usedItem);
        }

        void DisplayItemsInUI(Item pickedItem)
        {
            foreach (var prank in pickedItem.ItemParams.PrankParams)
            {
                itemsByPranks[prank] = pickedItem;
            }

            Systems.Instance.UISystem.ShowScreen(UIType.Inventory);
            IvnentoryUIParams uiParams = new();
            uiParams.Names = new string[Items.Count];
            uiParams.Amounts = new string[Items.Count];

            for (int i = 0; i < Items.Count; i++)
            {
                var item = Items[i];
                uiParams.Names[i] = item.IsUsedUp ? item.AlternativeName : Items[i].DisplayName;
                uiParams.Amounts[i] = Items[i].ChargesAmount.ToString() + '/' + Items[i].MaxChargesAmount.ToString();
            }

            Systems.Instance.UISystem.UpdateScreen(UIType.Inventory, uiParams);
        }
    }
}