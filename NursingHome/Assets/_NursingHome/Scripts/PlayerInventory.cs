using NursingHome.Interactions;
using System;
using System.Collections.Generic;
using NursingHome.UserInterface;

namespace NursingHome
{
    public class PlayerInventory
    {
        public event Action<ItemParams> OnItemAdded;

        public List<ItemParams> Items { get; private set; } = new List<ItemParams>();
        Dictionary<PrankParams, ItemParams> itemsByPranks = new Dictionary<PrankParams, ItemParams>();
        UIParams uiParams;

        public PlayerInventory(ItemPicker picker)
        {
            picker.OnItemPicked += HandleItemPicked;
        }

        public List<PrankParams> GetAvailablePranksForItem(ItemParams itemParams)
        {
            List<PrankParams> result = new List<PrankParams>();

            foreach(var prank in itemParams.PrankParams)
            {
                if(itemsByPranks.ContainsKey(prank))
                {
                    result.Add(prank);
                }
            }

            return result;
        }

        void HandleItemPicked(ItemParams pickedItem)
        {
            OnItemAdded?.Invoke(pickedItem);
            Items.Add(pickedItem);

            foreach(var prank in pickedItem.PrankParams)
            {
                itemsByPranks[prank] = pickedItem;
            }

            Systems.Instance.UISystem.ShowScreen(UIType.Inventory);
            uiParams.Names = new string[Items.Count];
            for(int i = 0; i < Items.Count; i++)
            {
                uiParams.Names[i] = Items[i].ItemName;
            }

            Systems.Instance.UISystem.UpdateScreen(UIType.Inventory, uiParams);
        }
    }
}