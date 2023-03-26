﻿using UnityEngine;

namespace NursingHome.Interactions
{
    public abstract class PrankParams : ScriptableObject
    {
        [field: SerializeField]
        public float ActivationDelay { get; private set; }

        [field:SerializeField]
        public string DisplayName { get; private set; }

        [field:SerializeField]
        public int PrankPoints { get; private set; }

        [field:SerializeField]
        public float LureRange { get; private set; }
    }
}