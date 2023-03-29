using Sirenix.OdinInspector;
using System;
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

        [SerializeField]
        [FoldoutGroup("Door Params")]
        Transform inwardsLeftPivot;

        [SerializeField]
        [FoldoutGroup("Door Params")]
        Transform outwardsLeftPivot;


        [Button]
        void SetDoorPivot(DoorType doorType)
        {
            DisablePivot(inwardsRightPivot);
            DisablePivot(outwardsLeftPivot);
            DisablePivot(outwardsLeftPivot);
            DisablePivot(inwardsLeftPivot);

            if (doorType == DoorType.RightInwards)
            {
                if (!inwardsRightPivot)
                    return;

                inwardsRightPivot.localEulerAngles = Vector3.zero;
                doors.parent = inwardsRightPivot;
                doors.localPosition = Vector3.zero;
                doors.localEulerAngles = Vector3.zero;
                inwardsRightPivot.name = "DOOR " + inwardsRightPivot.name;
                inwardsRightPivot.gameObject.SetActive(true);
            }

            if (doorType == DoorType.RightOutwards)
            {
                if (!outwardsRightPivot)
                    return;

                outwardsRightPivot.localEulerAngles = Vector3.zero;
                doors.parent = inwardsRightPivot;
                doors.localPosition = Vector3.zero;
                doors.localEulerAngles = Vector3.zero;
                doors.parent = outwardsRightPivot;
                outwardsRightPivot.name = "DOOR " + outwardsRightPivot.name;
                outwardsRightPivot.gameObject.SetActive(true);
            }

            if (doorType == DoorType.LeftOutwards)
            {
                if (!outwardsLeftPivot)
                    return;

                outwardsLeftPivot.localEulerAngles = new Vector3(0, 180, 0);
                doors.parent = inwardsLeftPivot;
                doors.localPosition = Vector3.zero;
                doors.localEulerAngles = Vector3.zero;
                doors.parent = outwardsLeftPivot;
                outwardsLeftPivot.name = "DOOR " + outwardsLeftPivot.name;
                outwardsLeftPivot.gameObject.SetActive(true);

            }

            if (doorType == DoorType.LeftInwards)
            {
                if (!inwardsLeftPivot)
                    return;

                inwardsLeftPivot.localEulerAngles = new Vector3(0, 180, 0);
                doors.parent = inwardsLeftPivot;
                doors.localPosition = Vector3.zero;
                doors.localEulerAngles = Vector3.zero;
                inwardsLeftPivot.name = "DOOR " + inwardsLeftPivot.name;
                inwardsLeftPivot.gameObject.SetActive(true);
            }
        }

        void DisablePivot(Transform pivotToDisable)
        {
            pivotToDisable.name = inwardsRightPivot.name.Replace("DOOR ", string.Empty);
            pivotToDisable.gameObject.SetActive(false);
        }
    }
}