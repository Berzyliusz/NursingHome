using NursingHome.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace NursingHome.AI
{
    public class IdleState : MonoBehaviour, IState
    {
        [SerializeField] Vector2 idleDurationMinMax = new Vector2(1, 5);

        Animator animator;
        NavMeshAgent navAgent;

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
        }

        void Awake()
        {
            animator = GetComponent<Animator>();
            navAgent = GetComponent<NavMeshAgent>();
        }
    }
}