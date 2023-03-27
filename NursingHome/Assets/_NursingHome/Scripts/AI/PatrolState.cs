using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class PatrolState : StateBase, IState
    {
        [SerializeField]
        Transform[] waypoints;

        public void StartState()
        {
            Vector3 randomWaypoint = waypoints[Random.Range(0, waypoints.Length)].position;
            ai.NavAgent.SetDestination(randomWaypoint);
            ai.Animator.SetBool(AnimationHashes.WalkHash, true);
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WalkHash, false);
        }

        public bool IsStateDone()
        {
            float distanceToTarget = Vector3.Distance(transform.position, ai.NavAgent.destination);
            return distanceToTarget < ai.InRangeDistance;
        }

        public void UpdateState()
        {

        }
    }
}