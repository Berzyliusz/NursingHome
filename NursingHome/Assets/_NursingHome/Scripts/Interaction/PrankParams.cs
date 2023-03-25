using UnityEngine;

namespace NursingHome.Interactions
{
    public abstract class PrankParams : ScriptableObject
    {
        [field:SerializeField]
        public string DisplayName { get; private set; }

        [field:SerializeField]
        public int PrankPoints { get; private set; }

        // We can add PrankParams instance to both:
        // the item we pick up, so it knows WHAT it allows
        // to the usable item, so whe know WHERE it allows

        // We can then compare object to object
        // And if we have a match, we can display that prank and use it
        // We can store logic, as to what happens with given item
    }
}