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
        [SerializeField]
        float chaseDistance = 10f;

        bool spottedPlayer;
        float searchTimer;

        public void StartState()
        {
            GoToRandomPointNearby();
            ai.Animator.SetBool(AnimationHashes.WalkHash, true);

            searchTimer = searchTime;
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WalkHash, false);
            spottedPlayer = false;
        }

        public bool IsStateDone()
        {
            return searchTimer <= 0;
        }

        public void UpdateState()
        {
            searchTimer -= Time.deltaTime;

            if(Vector3.Distance(transform.position, Systems.Instance.Player.GetPlayerPosition()) < chaseDistance)
            {
                spottedPlayer = true;
                return;
            }

            // TODO: Also look for and investigate patients
            // Consider checking line of sight too

            if (Vector3.Distance(transform.position, ai.NavAgent.destination) < ai.InRangeDistance)
            {
                GoToRandomPointNearby();
            }
        }

        public bool SpottedPlayer()
        {
            return spottedPlayer;
        }

        void GoToRandomPointNearby()
        {
            Vector3 point = FindNearbyRandomPoint();
            ai.NavAgent.SetDestination(point);
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