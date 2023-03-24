using UnityEngine;

namespace NursingHome.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class InteractableItem : MonoBehaviour
    {
        // Hold a reference for what pranks does this item allow

        public void Pickup()
        {
            // Add to player inventory
            Destroy(gameObject);
        }
    }
}