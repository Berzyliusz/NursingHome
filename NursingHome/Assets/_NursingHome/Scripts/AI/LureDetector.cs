using NursingHome.Interactions;
using UnityEngine;

namespace NursingHome
{
    public class LureDetector : MonoBehaviour
    {
        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag(Tags.Lure))
                return;


            Debug.Log($"Lure TRIGGER detected {other.gameObject.name}");
        }
    }
}