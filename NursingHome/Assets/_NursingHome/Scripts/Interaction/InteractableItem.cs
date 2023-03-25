using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class InteractableItem : MonoBehaviour
    {
        public string DisplayName => itemParams.ItemName;
        public ItemParams ItemParams => itemParams;

        [SerializeField]
        [InlineEditor]
        ItemParams itemParams;

        public void Pickup()
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 0.01f);
        }
    }
}