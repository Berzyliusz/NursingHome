using UnityEngine;

namespace NursingHome.UserInterface
{
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
            DestroySpawnedItems();

            foreach (var itemName in uiParams.Names)
            {
                var newItem = GameObject.Instantiate(itemPrefab, spawnParent.transform);
                newItem.SetItemText(itemName);
                spawnedItems.Add(newItem);
            }
        }
    }
}