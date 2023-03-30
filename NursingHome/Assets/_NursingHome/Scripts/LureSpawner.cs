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
        public LureSpawner(ItemUser itemUser)
        {
            itemUser.OnItemUsed += HandleItemUsed;
        }

        void HandleItemUsed(UsableElement prankedElement, Item item, PrankParams prankParams)
        {
            var parent = prankedElement.Waypoint == null ? prankedElement.transform : prankedElement.Waypoint.transform;
            Lure lure = CreateLureObject(parent, prankParams);
            lure.SetupLure(prankParams, parent);
        }

        Lure CreateLureObject(Transform parent, PrankParams prank)
        {
            var lureObj = new GameObject("Lure");

            lureObj.tag = Tags.Lure;
            var lureLayer = LayerMask.NameToLayer(Tags.Lure);
            lureObj.layer = lureLayer;

            lureObj.transform.parent = parent;
            lureObj.transform.localPosition = Vector3.zero;

            Lure lure = AddComponentsToLure(lureObj);
            return lure;
        }

        Lure AddComponentsToLure(GameObject lureObj)
        {
            var lure = lureObj.AddComponent<Lure>();
            var trigger = lureObj.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            return lure;
        }
    }
}