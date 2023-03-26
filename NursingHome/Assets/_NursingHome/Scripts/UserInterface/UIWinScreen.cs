using TMPro;
using UnityEngine;

namespace NursingHome.UserInterface
{
    public class UIWinScreen : PoolingUIElement
    {
        public override UIType Type => UIType.WinScreen;

        [SerializeField]
        TextMeshProUGUI totalScoreText;

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

            totalScoreText.text = "Total points: " + uiParams.Name;

            foreach(var entry in uiParams.Names)
            {
                var spawnedItem = Instantiate(itemPrefab, spawnParent);
                spawnedItems.Add(spawnedItem);
                spawnedItem.SetItemText(entry);
            }
        }
    }
}