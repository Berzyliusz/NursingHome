using UnityEngine;

namespace NursingHome.AI
{
    public class TravelToDestinationState : MonoBehaviour, IState
    {
        public void EndState()
        {
            
        }

        public bool IsStateDone()
        {
            // wait till we are close to the target
            return false;
        }

        public void StartState()
        {
            // We need to get our destination from somewhere
            // and set destination there
        }

        public void UpdateState()
        {
            // Check the distance to target
        }
    }
}