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

        DoorType currentDoorType;
        Transform currentPivot;

        [Button("Right Inwards", ButtonSizes.Large)]
        void SetInwardRightPivot()
        {
            SetDoorPivot(DoorType.RightInwards);
        }

        [Button("Right Outwards", ButtonSizes.Large)]
        void SetOutwardRightPivot()
        {
            SetDoorPivot(DoorType.RightOutwards);
        }

        [Button("Left Inwards", ButtonSizes.Large)]
        void SetInwardsLeftPivot()
        {
            SetDoorPivot(DoorType.LeftInwards);
        }

        [Button("Left Outwards", ButtonSizes.Large)]
        void SetOutwardsLeftPivot()
        {
            SetDoorPivot(DoorType.LeftOutwards);
        }

        [PropertySpace(40)]
        [Button("Open Halfway", ButtonSizes.Large)]
        void OpenHalfway()
        {
            float halfWayRotation = 90;
            switch(currentDoorType)
            {
                case DoorType.LeftInwards:
                    currentPivot.Rotate(0, -halfWayRotation, 0);
                    break;
                case DoorType.LeftOutwards:
                    currentPivot.Rotate(0, halfWayRotation, 0);
                    break;
                case DoorType.RightInwards:
                    currentPivot.Rotate(0, -halfWayRotation, 0);
                    break;
                case DoorType.RightOutwards:
                    currentPivot.Rotate(0, halfWayRotation, 0);
                    break;
            }
        }

        [Button("Open Fully", ButtonSizes.Large)]
        void OpenFully()
        {
            float fullRotation = 145;
            switch (currentDoorType)
            {
                case DoorType.LeftInwards:
                    currentPivot.Rotate(0, -fullRotation, 0);
                    break;
                case DoorType.LeftOutwards:
                    currentPivot.Rotate(0, fullRotation, 0);
                    break;
                case DoorType.RightInwards:
                    currentPivot.Rotate(0, -fullRotation, 0);
                    break;
                case DoorType.RightOutwards:
                    currentPivot.Rotate(0, fullRotation, 0);
                    break;
            }
        }

        [Button("Open By Degrees", ButtonSizes.Large)]
        void OpenFully(float openingDegrees)
        {
            switch (currentDoorType)
            {
                case DoorType.LeftInwards:
                    currentPivot.Rotate(0, -openingDegrees, 0);
                    break;
                case DoorType.LeftOutwards:
                    currentPivot.Rotate(0, openingDegrees, 0);
                    break;
                case DoorType.RightInwards:
                    currentPivot.Rotate(0, -openingDegrees, 0);
                    break;
                case DoorType.RightOutwards:
                    currentPivot.Rotate(0, openingDegrees, 0);
                    break;
            }
        }

        void SetDoorPivot(DoorType doorType)
        {
            DisablePivot(inwardsRightPivot);
            DisablePivot(outwardsRightPivot);
            DisablePivot(outwardsLeftPivot);
            DisablePivot(inwardsLeftPivot);

            currentDoorType = doorType;

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
                currentPivot = inwardsRightPivot;
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
                currentPivot = outwardsRightPivot;
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
                currentPivot = outwardsLeftPivot;

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
                currentPivot = inwardsLeftPivot;
            }
        }

        void DisablePivot(Transform pivotToDisable)
        {
            pivotToDisable.name = pivotToDisable.name.Replace("DOOR ", string.Empty);
            pivotToDisable.gameObject.SetActive(false);
        }
    }
}