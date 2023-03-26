using NursingHome.Interactions;
using UnityEngine;

namespace NursingHome.Lures
{
    public class Lure : MonoBehaviour
    {
        PrankParams prank;

        public void SetPrankParams(PrankParams prank)
        {
            this.prank = prank;
        }
    }
}