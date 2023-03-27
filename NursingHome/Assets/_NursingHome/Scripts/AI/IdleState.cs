using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class IdleState : StateBase, IState
    {
        [SerializeField] Vector2 idleDurationMinMax = new Vector2(1, 5);

        float idleTimer;

        public void StartState()
        {
            idleTimer = Random.Range(idleDurationMinMax.x, idleDurationMinMax.y);
            ai.Animator.SetBool(AnimationHashes.IdleHash, true);
        }

        public void EndState()
        {
            idleTimer = float.MaxValue;
            ai.Animator.SetBool(AnimationHashes.IdleHash, false);
        }

        public bool IsStateDone()
        {
            return idleTimer <= 0;
        }

        public void UpdateState()
        {
            idleTimer -= Time.deltaTime;

            // Todo: Expand to use idle breakers, or some more interesting idles
        }
    }
}