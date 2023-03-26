using NursingHome.Interactions;
using NursingHome.Lures;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome
{
    public class LureDetector : MonoBehaviour
    {
        public event Action<Lure> OnLureDetectionEnter;
        public event Action<Lure> OnLureDetectionExit;

        Dictionary<Collider, Lure> cachedLures = new Dictionary<Collider, Lure>();

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Lure))
                return;

            if(!cachedLures.ContainsKey(other))
            {
                cachedLures.Add(other, other.GetComponent<Lure>());
            }

            OnLureDetectionEnter?.Invoke(cachedLures[other]);
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.Lure))
                return;

            if (!cachedLures.ContainsKey(other))
            {
                cachedLures.Add(other, other.GetComponent<Lure>());
            }

            OnLureDetectionExit?.Invoke(cachedLures[other]);
        }
    }
}