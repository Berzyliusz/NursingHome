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
            var selectedItem = interactionDetector.GetUsableItem();

            if(selectedItem)
            {
                var usableItem = (UsableItem)selectedItem;
                var parent = usableItem.Waypoint == null ? usableItem.transform : usableItem.Waypoint.transform;
                Lure lure = CreateLureObject(parent, prankParams);
                lure.SetupLure(prankParams, parent);
            }
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