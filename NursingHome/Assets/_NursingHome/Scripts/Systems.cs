using UnityEngine;
using NursingHome.UserInterface;
using System;
using NursingHome.Interactions;

namespace NursingHome
{
    public class Systems : MonoBehaviour
    {
        public static Systems Instance { get; private set; }

        [field:SerializeField]
        public UISystem UISystem { get; private set; }

        [SerializeField]
        ItemPicker itemPicker;

        PlayerInventory inventory;

        void Awake()
        {
            Instance = this;
            inventory = new PlayerInventory(itemPicker);
        }
    }
}