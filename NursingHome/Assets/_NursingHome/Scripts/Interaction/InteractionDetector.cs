using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NursingHome.Interactions
{
    public interface IInteractionDetector
    {
        void SetCanDetectInteraction(bool canDetectInteraction);
        PickupElement GetPickupElement();
        UsableElement GetUsableElement();
    }

    public class InteractionDetector : MonoBehaviour, IInteractionDetector
    {
        [SerializeField] float rayLength = 0.5f;
        [SerializeField] LayerMask layerMask;

        public static event Action<InteractableElement> OnInteractionDetected;
        public event Action<InteractableElement> OnInteracted;

        InteractableElement selectedItem;
        InteractableElement previousItem;

        bool canDetect;

        Dictionary<Transform, InteractableElement> cachedItems = new Dictionary<Transform, InteractableElement>();

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
                    cachedItems[hit.transform] = hit.transform.GetComponent<InteractableElement>();
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

        public PickupElement GetPickupElement()
        {
            if(selectedItem is PickupElement)
                return (PickupElement)selectedItem;

            return null;
        }

        public UsableElement GetUsableElement()
        {
            if(selectedItem is UsableElement)
                return ((UsableElement)selectedItem);

            return null;
        }
    }
}