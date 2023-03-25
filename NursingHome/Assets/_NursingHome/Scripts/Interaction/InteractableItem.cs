using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class InteractableItem : MonoBehaviour
    {
        public string DisplayName => itemParams.ItemName;

        [SerializeField]
        [InlineEditor]
        ItemParams itemParams;

        public void Pickup()
        {
            // Add to player inventory
            Destroy(gameObject);
        }
    }
}