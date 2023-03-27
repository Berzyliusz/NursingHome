using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class IdleState : StateBase, IState
    {
        [SerializeField] Vector2 idleDurationMinMax = new Vector2(1, 5);

        Animator animator;

        float timer;
        float idleTime;

        public void StartState()
        {
            idleTime = Random.Range(idleDurationMinMax.x, idleDurationMinMax.y);
            timer = 0;

            animator.SetBool(AnimationHashes.IdleHash, true);
        }

        public void EndState()
        {
            timer = 0;
            idleTime = 0;

            animator.SetBool(AnimationHashes.IdleHash, false);
        }

        public bool IsStateDone()
        {
            return timer > idleTime;
        }

        public void UpdateState()
        {
            timer += Time.deltaTime;

            // Todo: Expand to use idle breakers, or some more interesting idles
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
        }
    }
}