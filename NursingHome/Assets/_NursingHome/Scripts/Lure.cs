using NursingHome.Interactions;
using UnityEngine;

namespace NursingHome.Lures
{
    public class Lure : MonoBehaviour
    {
        public PrankParams Prank { get; private set; }

        public void SetPrankParams(PrankParams prank)
        {
            this.Prank = prank;
        }
    }
}