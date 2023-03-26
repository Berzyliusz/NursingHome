using Sirenix.OdinInspector;
using UnityEngine;

namespace NursingHome.Interactions
{
    [CreateAssetMenu(menuName = "NursingHome/Pranks/Spawn Effects Delayed")]
    public class SpawnEffectsDelayed : PrankParams
    {
        [field: SerializeField]
        public float ActivationDelay { get; private set; }

        [SerializeField]
        [AssetsOnly]
        GameObject prefabToSpawn;
    }
}