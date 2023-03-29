using Sirenix.OdinInspector;
using UnityEngine;

namespace Utilities
{
    public class DoorSetter : MonoBehaviour
    {
        enum DoorType
        {
            RightInwards,
            RightOutwards,
            LeftInwards,
            LeftOutwards,
        };

        [SerializeField]
        [FoldoutGroup("Door Params")]
        Transform doors;

        [SerializeField]
        [FoldoutGroup("Door Params")]
        Transform outwardsPivot;

        void SetDoorPivot(DoorType doorType)
        {

            if(doorType == DoorType.RightInwards)
            {
                doors.parent = this.transform;
                // set the "starting" local position for right
            }

            if(doorType == DoorType.RightOutwards)
            {
                doors.parent = outwardsPivot;
                // set the "starting" local position for right
            }
        }
    }
}