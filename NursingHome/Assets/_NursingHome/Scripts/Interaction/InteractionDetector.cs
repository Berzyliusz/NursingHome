using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NursingHome.Interactions
{
    public interface IInteractionDetector
    {
        InteractableItem SelectedItem { get; }
        void SetCanDetectInteraction(bool canDetectInteraction);
    }

    public class InteractionDetector : MonoBehaviour, IInteractionDetector
    {
        [SerializeField] float rayLength = 0.5f;
        [SerializeField] LayerMask layerMask;

        public static event Action<InteractableItem> OnInteractionDetected;
        public event Action<InteractableItem> OnInteracted;

        public InteractableItem SelectedItem { get; private set; }
        InteractableItem previousItem;

        bool canDetect;

        Dictionary<Transform, InteractableItem> cachedItems = new Dictionary<Transform, InteractableItem>();

        public void SetCanDetectInteraction(bool canDetectInteraction)
        {
            canDetect = canDetectInteraction;

            if(!canDetect)
                SelectedItem= null;
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

                SelectedItem = cachedItems[hit.transform];
            }
            else
            {
                SelectedItem = null;
            }

            if (SelectedItem != previousItem)
            {
                OnInteractionDetected?.Invoke(SelectedItem);
                OnInteracted?.Invoke(SelectedItem);
                previousItem = SelectedItem;
            }
        }
    }
}