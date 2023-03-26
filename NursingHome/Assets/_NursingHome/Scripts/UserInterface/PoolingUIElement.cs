using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public abstract class PoolingUIElement : UIElement
    {
        [SerializeField]
        [AssetsOnly]
        protected UISingleItem itemPrefab;

        [SerializeField]
        [SceneObjectsOnly]
        protected Transform spawnParent;

        protected List<UISingleItem> spawnedItems = new List<UISingleItem>();

        protected void DestroySpawnedItems()
        {
            while (spawnedItems.Count > 0)
            {
                Destroy(spawnedItems[0].gameObject);
                spawnedItems.RemoveAt(0);
            }
        }
    }
}