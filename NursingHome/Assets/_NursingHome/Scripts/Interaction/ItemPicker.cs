using System;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class ItemPicker : MonoBehaviour
    {
        [SerializeField]
        InteractionDetector interactionDetector;

        public event Action<ItemParams> OnItemPicked;

        void Update()
        {
            //TODO:
            // Update only when we need to]
            // get inputs from systems
            var item = interactionDetector.SelectedItem;
            if (item == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                item.Pickup();
                OnItemPicked?.Invoke(item.ItemParams);
            }
        }
    }
}