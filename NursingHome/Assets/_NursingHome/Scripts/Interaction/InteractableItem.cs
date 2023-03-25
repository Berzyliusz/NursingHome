using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace NursingHome.Interactions
{
    [RequireComponent(typeof(Collider))]
    public class InteractableItem : MonoBehaviour
    {
        public static event Action<ItemParams> OnItemPicked;

        public string DisplayName => itemParams.ItemName;

        [SerializeField]
        [InlineEditor]
        ItemParams itemParams;

        public void Pickup()
        {
            OnItemPicked?.Invoke(itemParams);
            Destroy(gameObject);
        }
    }
}