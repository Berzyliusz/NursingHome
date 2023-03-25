using System;
using UnityEngine;

namespace NursingHome.Interactions
{
    public class ItemPicker : MonoBehaviour
    {
        InteractionDetector interactionDetector;

        public event Action<ItemParams> OnItemPicked;

        IInputs inputs;

        void Start()
        {
            interactionDetector = Systems.Instance.InteractionDetector;
            inputs = Systems.Instance.Inputs;
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
            if (inputs.Use)
            {
                var item = interactionDetector.SelectedItem;
                item.UseItem();
                OnItemPicked?.Invoke(item.ItemParams);
            }
        }
    }
}