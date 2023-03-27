using NursingHome.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace NursingHome.AI
{
    public class InvestigateState : StateBase, IState
    {
        Animator animator;
        NavMeshAgent navAgent;

        public void StartState()
        {
            var lure = ai.StrongestLure;
            animator.SetBool(AnimationHashes.WalkHash, true);
            navAgent.SetDestination(lure.transform.position);

            Debug.Log("Starting investigation");
        }

        public void EndState()
        {
            animator.SetBool(AnimationHashes.WalkHash, false);

            // store investigated prank so we know what to look for

            // Todo: Get angry, and increase speed / range detection

            Debug.Log("Ending investigation");
        }

        public bool IsStateDone()
        {
            float distanceToTarget = Vector3.Distance(transform.position, navAgent.destination);
            return distanceToTarget < ai.InRangeDistance;

        }

        public void UpdateState()
        {
            //TODO: Add roation to given waypoint, so we are facing right.
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        }
    }
}