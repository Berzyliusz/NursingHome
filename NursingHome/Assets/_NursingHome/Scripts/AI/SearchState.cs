using NursingHome.Animations;
using UnityEngine;

namespace NursingHome.AI
{
    public class SearchState : StateBase, IState
    {
        //TODO: Moe to aiParams
        [SerializeField]
        float searchTime = 10f;
        [SerializeField]
        Vector2 randomPointRanges = new Vector2(-5, 5);

        bool spottedPlayer;
        float searchTimer;

        public void StartState()
        {
            GoToRandomPointNearby();
            ai.Animator.SetBool(AnimationHashes.WalkHash, true);
            ai.Emotions.SetEmotion(EmotionType.Angry);
            searchTimer = searchTime;
            ai.Eyes.SetEyesightChecking(true);
        }

        public void EndState()
        {
            ai.Animator.SetBool(AnimationHashes.WalkHash, false);
            ai.Emotions.SetEmotion(EmotionType.None);
            ai.Eyes.SetEyesightChecking(false);

            spottedPlayer = false;
        }

        public bool IsStateDone()
        {
            return searchTimer <= 0;
        }

        public void UpdateState()
        {
            searchTimer -= Time.deltaTime;

            if(ai.Eyes.CanSeePlayer)
            {
                spottedPlayer = true;
                return;
            }

            // TODO: Also look for and investigate patients

            if (Vector3.Distance(transform.position, ai.NavAgent.destination) < ai.Params.InRangeDistance)
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