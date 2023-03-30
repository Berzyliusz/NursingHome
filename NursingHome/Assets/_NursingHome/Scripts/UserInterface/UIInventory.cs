using UnityEngine;

namespace NursingHome.UserInterface
{
    public class IvnentoryUIParams : UIParams
    {
        public string[] Amounts;
    }

    public class UIInventory : PoolingUIElement
    {
        public override UIType Type => UIType.Inventory;

        public override void Hide()
        {
            parent.gameObject.SetActive(false);
        }

        public override void Show()
        {
            parent.gameObject.SetActive(true);
        }

        public override void UpdateUI(UIParams uiParams)
        {
            if (uiParams is not IvnentoryUIParams)
                return;

            var inventoryParams = (IvnentoryUIParams)uiParams;

            DestroySpawnedItems();

            for (int i = 0; i < inventoryParams.Names.Length; i++)
            {
                string itemName = inventoryParams.Names[i];
                var newItem = GameObject.Instantiate(itemPrefab, spawnParent.transform);
                newItem.SetItemText(itemName);
                newItem.SetSubtext(inventoryParams.Amounts[i]);
                spawnedItems.Add(newItem);
            }
        }
    }
}