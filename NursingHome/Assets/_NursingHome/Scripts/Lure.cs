using NursingHome.Interactions;
using System.Collections;
using UnityEngine;

namespace NursingHome.Lures
{
    public class Lure : MonoBehaviour
    {
        public PrankParams Prank { get; private set; }
        public Transform Waypoint { get; private set; }

        SphereCollider sphereCollider;

        public void SetupLure(PrankParams prank, Transform waypoint)
        {
            this.Prank = prank;
            Waypoint = waypoint;

            sphereCollider = GetComponent<SphereCollider>();
            sphereCollider.radius = 0.001f;
            StartCoroutine(TriggerGrowRoutine());
        }

        IEnumerator TriggerGrowRoutine()
        {
            WaitForEndOfFrame wait = new WaitForEndOfFrame();

            yield return new WaitForSeconds(Prank.ActivationDelay);

            var targetRadius = Prank.LureRange;

            while(sphereCollider.radius < targetRadius )
            {
                sphereCollider.radius += 0.1f;
                yield return wait;
            }

            sphereCollider.radius = targetRadius;
        }
    }
}