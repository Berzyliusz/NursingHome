using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NursingHome.UserInterface;

namespace NursingHome
{
    public class Systems : MonoBehaviour
    {
        public static Systems Instance { get; private set; }

        [SerializeField]
        UI ui;

        void Awake()
        {
            Instance = this;
            // Create UI system
            // Pass references to other systems
        }
    }
}