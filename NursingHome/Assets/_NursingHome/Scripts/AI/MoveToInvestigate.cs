using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class MoveToInvestigate : StateBase, IState
    {
        float distanceToTarget;

        public void StartState()
        {
            var lure = ai.StrongestLure;
            ai.Animator.SetBool(AnimationHashes.WalkHash, true);
            ai.NavAgent.SetDestination(lure.transform.position);
            distanceToTarget = float.MaxValue;

            Debug.Log("Moving to investigate");

        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WalkHash, false);
        }

        public bool IsStateDone()
        {
            return distanceToTarget < ai.InRangeDistance;
        }

        public void UpdateState()
        {
            distanceToTarget = Vector3.Distance(transform.position, ai.NavAgent.destination);
        }
    }
}