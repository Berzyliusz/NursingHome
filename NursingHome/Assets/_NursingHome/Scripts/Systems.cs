using UnityEngine;
using NursingHome.UserInterface;

namespace NursingHome
{
    public class Systems : MonoBehaviour
    {
        public static Systems Instance { get; private set; }

        [field:SerializeField]
        public UISystem UISystem { get; private set; } 

        void Awake()
        {
            Instance = this;
            // Pass references to other systems
        }
    }
}