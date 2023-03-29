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
        Transform inwardsRightPivot;

        [SerializeField]
        [FoldoutGroup("Door Params")]
        Transform outwardsRightPivot;

        [Button]
        void SetDoorPivot(DoorType doorType)
        {
            if(doorType == DoorType.RightInwards)
            {
                if (!inwardsRightPivot)
                    return;

                doors.parent = inwardsRightPivot;
                doors.localPosition = Vector3.zero;
                doors.localEulerAngles = Vector3.zero;
            }

            if(doorType == DoorType.RightOutwards)
            {
                if (!outwardsRightPivot)
                    return;

                doors.parent = outwardsRightPivot;
                doors.localPosition = Vector3.zero;
                doors.localEulerAngles = Vector3.zero;
            }
        }
    }
}