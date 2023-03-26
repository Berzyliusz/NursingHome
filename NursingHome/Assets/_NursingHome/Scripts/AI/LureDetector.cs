using NursingHome.Interactions;
using NursingHome.Lures;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome
{
    public class LureDetector : MonoBehaviour
    {
        public event Action<Lure> OnLureEnter;
        public event Action<Lure> OnLureExit;

        Dictionary<Collider, Lure> cachedLures = new Dictionary<Collider, Lure>();

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Lure))
                return;

            if(!cachedLures.ContainsKey(other))
            {
                cachedLures.Add(other, other.GetComponent<Lure>());
            }

            OnLureEnter?.Invoke(cachedLures[other]);
        }

        void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag(Tags.Lure))
                return;

            if (!cachedLures.ContainsKey(other))
            {
                cachedLures.Add(other, other.GetComponent<Lure>());
            }

            OnLureExit?.Invoke(cachedLures[other]);
        }
    }
}