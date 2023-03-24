using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NursingHome.Interactions
{
    public class InteractionDetector : MonoBehaviour
    {
        [SerializeField] float rayLength = 0.5f;
        [SerializeField] LayerMask layerMask;

        public InteractableItem SelectedItem { get; private set; }

        Dictionary<Transform, InteractableItem> cachedItems = new Dictionary<Transform, InteractableItem>();

        void Update()
        {
            var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, rayLength, layerMask))
            {
                if(!cachedItems.ContainsKey(hit.transform))
                {
                    cachedItems[hit.transform] = hit.transform.GetComponent<InteractableItem>();
                }

                if (cachedItems[hit.transform])
                {
                    SelectedItem = cachedItems[hit.transform];
                }                
            }
        }
    }
}