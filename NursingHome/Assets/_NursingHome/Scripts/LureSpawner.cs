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
            var selectedItem = interactionDetector.SelectedItem;

            if(selectedItem is UsableItem)
            {
                var usableItem = (UsableItem)selectedItem;
                var parent = usableItem.Waypoint == null ? usableItem.transform : usableItem.Waypoint.transform;
                Lure lure = CreateLureObject(parent, prankParams);
                lure.SetupLure(prankParams, parent);
            }
            else
            {
                throw new InvalidCastException("Lures can only be spawned on usable items!");
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

            Lure lure = AddComponentsToLure(prank, lureObj);
            return lure;
        }

        Lure AddComponentsToLure(PrankParams prank, GameObject lureObj)
        {
            var lure = lureObj.AddComponent<Lure>();
            var trigger = lureObj.AddComponent<SphereCollider>();
            trigger.isTrigger = true;
            return lure;
        }
    }
}