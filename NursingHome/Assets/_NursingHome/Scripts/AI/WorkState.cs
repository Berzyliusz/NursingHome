using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class WorkState : StateBase, IState
    {
        public void StartState()
        {
            ai.Animator.SetBool(AnimationHashes.WorkHash, true);

            Debug.Log("Work state");
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WorkHash, false);
        }

        public bool IsStateDone()
        {
            return false;
        }

        public void UpdateState()
        {
            
        }
    }
}