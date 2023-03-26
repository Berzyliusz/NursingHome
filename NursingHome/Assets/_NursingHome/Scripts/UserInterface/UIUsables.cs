using UnityEngine;

namespace NursingHome.UserInterface
{
    public class UIUsables : PoolingUIElement
    {
        public override UIType Type => UIType.Use;

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
                newItem.SetItemName(itemName);
                spawnedItems.Add(newItem);
            }
        }
    }
}