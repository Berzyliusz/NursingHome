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
            InteractionDetector.OnInteractionDetected += HandleInteractionDetected;
            enabled = false;
        }

        void OnDestroy()
        {
            InteractionDetector.OnInteractionDetected -= HandleInteractionDetected;
        }

        void HandleInteractionDetected(InteractableItem interactedItem)
        {
            if (interactedItem == null || !interactedItem.gameObject.CompareTag(Tags.Pickable))
            {
                enabled = false;
            }
            else
            {
                enabled = true;
            }
        }

        void Update()
        {
            //TODO:
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