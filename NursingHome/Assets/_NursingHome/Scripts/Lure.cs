using NursingHome.Interactions;
using System.Collections;
using UnityEngine;

namespace NursingHome.Lures
{
    public class Lure : MonoBehaviour
    {
        public PrankParams Prank { get; private set; }

        SphereCollider collider;

        public void SetupLure(PrankParams prank)
        {
            this.Prank = prank;

            collider = GetComponent<SphereCollider>();
            collider.radius = 0.001f;
            StartCoroutine(TriggerGrowRoutine());
        }

        IEnumerator TriggerGrowRoutine()
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();

            yield return new WaitForSeconds(Prank.ActivationDelay);

            var targetRadius = Prank.LureRange;

            while(collider.radius < targetRadius )
            {
                collider.radius += 0.1f;
                yield return wait;
            }

            collider.radius = targetRadius;
        }
    }
}