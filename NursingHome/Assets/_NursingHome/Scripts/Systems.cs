using UnityEngine;
using NursingHome.UserInterface;
using NursingHome.Interactions;

namespace NursingHome
{
    public class Systems : MonoBehaviour
    {
        public static Systems Instance { get; private set; }

        [field:SerializeField]
        public UISystem UISystem { get; private set; }
        [field: SerializeField]
        public InteractionDetector InteractionDetector { get; private set; }

        [SerializeField]
        ItemPicker itemPicker;


        public PlayerInventory Inventory { get; private set; }

        void Awake()
        {
            Instance = this;
            Inventory = new PlayerInventory(itemPicker);
        }
    }
}