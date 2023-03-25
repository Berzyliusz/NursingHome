using System;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class ItemPicker : MonoBehaviour
    {
        InteractionDetector interactionDetector;

        public event Action<ItemParams> OnItemPicked;

        void Start()
        {
            interactionDetector = Systems.Instance.InteractionDetector;
        }

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
                item.UseItem();
                OnItemPicked?.Invoke(item.ItemParams);
            }
        }
    }
}