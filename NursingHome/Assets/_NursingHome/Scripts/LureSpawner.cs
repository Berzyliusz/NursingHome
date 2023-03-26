using NursingHome.Interactions;
using System;
using UnityEngine;

namespace NursingHome.Lures
{
    public interface ILureSpawner
    {
        
    }

    public class LureSpawner : ILureSpawner
    {
        readonly IInteractionDetector interactionDetector;

        public LureSpawner(ItemUser itemUser, IInteractionDetector interactionDetector)
        {
            itemUser.OnItemUsed += HandleItemUsed;
            this.interactionDetector = interactionDetector;
        }

        void HandleItemUsed(ItemParams itemParams, PrankParams prankParams)
        {
            Debug.Log("Spawning lure");
            Lure lure = CreateLureObject(interactionDetector.SelectedItem.transform, prankParams);

            // arm that lure with params of what staff should be looking for
        }

        Lure CreateLureObject(Transform parent, PrankParams prank)
        {
            var lureObj = new GameObject("Lure");
            lureObj.transform.parent = parent;
            lureObj.tag = Tags.Lure;

            var lureLayer = LayerMask.NameToLayer(Tags.Lure);
            lureObj.layer = lureLayer;
            
            lureObj.transform.localPosition = Vector3.zero;
            //Todo: We may need to add some kind of waypoint, so we know where to go

            var lure = lureObj.AddComponent<Lure>();
            var trigger = lureObj.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            trigger.radius = prank.LureRange;
            return lure;
        }
    }

    public class Lure : MonoBehaviour
    {

    }
}