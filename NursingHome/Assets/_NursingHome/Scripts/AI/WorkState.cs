using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class WorkState : StateBase, IState
    {
        [SerializeField] Vector2 workDurationMinMax = new Vector2(1, 5);
        [SerializeField] float rotationSpeed = 1.0f;

        float workTimer;

        public void StartState()
        {
            workTimer = Random.Range(workDurationMinMax.x, workDurationMinMax.y);
            ai.Animator.SetBool(AnimationHashes.WorkHash, true);
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WorkHash, false);
        }

        public bool IsStateDone()
        {
            return workTimer <= 0;
        }

        public void UpdateState()
        {
            workTimer -= Time.deltaTime;

            var lureWaypoint = ai.StrongestLure.Waypoint;
            var lureRotation = lureWaypoint.transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, lureRotation, Time.deltaTime * rotationSpeed);
        }
    }
}