using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public class UIInventory : UIElement
    {
        [SerializeField]
        [AssetsOnly]
        UISingleItem itemPrefab;

        List<UISingleItem> spawnedItems = new List<UISingleItem>();

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
            while(spawnedItems.Count > 0)
            {
                Destroy(spawnedItems[0].gameObject);
                spawnedItems.RemoveAt(0);
            }

            foreach(var itemName in uiParams.Names)
            {
                var newItem = GameObject.Instantiate(itemPrefab, parent.transform);
                newItem.SetItemName(itemName);
                spawnedItems.Add(newItem);
            }
        }
    }
}