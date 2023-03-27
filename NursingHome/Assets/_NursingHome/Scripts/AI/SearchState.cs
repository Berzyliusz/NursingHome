using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class SearchState : StateBase, IState
    {
        [SerializeField]
        float searchTime = 10f;
        [SerializeField]
        Vector2 randomPointRanges = new Vector2(-5, 5);

        float searchTimer;

        public void StartState()
        {
            // Check if player is in range or visible 
            // if so, go to chase state
            // else
            Vector3 point = FindNearbyRandomPoint();
            ai.NavAgent.SetDestination(point);
            ai.Animator.SetBool(AnimationHashes.WalkHash, true);

            searchTimer = searchTime;
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WalkHash, false);
        }

        public bool IsStateDone()
        {
            // or we arrived to search target
            return searchTimer <= 0;
        }

        public void UpdateState()
        {
            searchTimer -= Time.deltaTime;
        }

        Vector3 FindNearbyRandomPoint()
        {
            var randomPoint = transform.position;

            randomPoint.x += UnityEngine.Random.Range(randomPointRanges.x, randomPointRanges.y);
            randomPoint.z += UnityEngine.Random.Range(randomPointRanges.x, randomPointRanges.y);

            return randomPoint;
        }
    }
}