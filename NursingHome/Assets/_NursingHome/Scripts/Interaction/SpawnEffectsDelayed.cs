﻿using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    [CreateAssetMenu(menuName = "NursingHome/Pranks/Spawn Effects Delayed")]
    public class SpawnEffectsDelayed : PrankParams
    {
        [SerializeField]
        float activationDelay;

        [SerializeField]
        [AssetsOnly]
        GameObject prefabToSpawn;
    }
}