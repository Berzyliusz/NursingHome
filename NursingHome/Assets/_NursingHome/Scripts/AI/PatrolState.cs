using NursingHome.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace NursingHome.AI
{
    public class PatrolState : StateBase, IState
    {
        [SerializeField]
        Transform[] waypoints;
        [SerializeField]
        float inRangeDistance = 0.2f;

        Animator animator;
        NavMeshAgent navAgent;

        public void StartState()
        {
            Vector3 randomWaypoint = waypoints[Random.Range(0, waypoints.Length)].position;
            navAgent.SetDestination(randomWaypoint);
            animator.SetBool(AnimationHashes.WalkHash, true);
        }

        public void EndState()
        {
            animator.SetBool(AnimationHashes.WalkHash, false);
        }

        public bool IsStateDone()
        {
            float distanceToTarget = Vector3.Distance(transform.position, navAgent.destination);
            return distanceToTarget < inRangeDistance;
        }

        public void UpdateState()
        {

        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        }
    }
}