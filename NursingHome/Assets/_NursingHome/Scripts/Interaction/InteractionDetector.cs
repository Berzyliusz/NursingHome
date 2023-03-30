using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NursingHome.Interactions
{
    public interface IInteractionDetector
    {
        void SetCanDetectInteraction(bool canDetectInteraction);
        PickupItem GetPickupItem();
        UsableItem GetUsableItem();
    }

    public class InteractionDetector : MonoBehaviour, IInteractionDetector
    {
        [SerializeField] float rayLength = 0.5f;
        [SerializeField] LayerMask layerMask;

        public static event Action<InteractableItem> OnInteractionDetected;
        public event Action<InteractableItem> OnInteracted;

        InteractableItem selectedItem;
        InteractableItem previousItem;

        bool canDetect;

        Dictionary<Transform, InteractableItem> cachedItems = new Dictionary<Transform, InteractableItem>();

        public void SetCanDetectInteraction(bool canDetectInteraction)
        {
            canDetect = canDetectInteraction;

            if(!canDetect)
                selectedItem= null;
        }

        void Update()
        {
            if (!canDetect)
                return;

            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                if(!cachedItems.ContainsKey(hit.transform))
                {
                    cachedItems[hit.transform] = hit.transform.GetComponent<InteractableItem>();
                }

                selectedItem = cachedItems[hit.transform];
            }
            else
            {
                selectedItem = null;
            }

            if (selectedItem != previousItem)
            {
                OnInteractionDetected?.Invoke(selectedItem);
                OnInteracted?.Invoke(selectedItem);
                previousItem = selectedItem;
            }
        }

        public PickupItem GetPickupItem()
        {
            if(selectedItem is PickupItem)
                return (PickupItem)selectedItem;

            return null;
        }

        public UsableItem GetUsableItem()
        {
            if(selectedItem is UsableItem)
                return (UsableItem)selectedItem;

            return null;
        }
    }
}