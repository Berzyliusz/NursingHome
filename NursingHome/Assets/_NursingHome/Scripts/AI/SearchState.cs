using NursingHome.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.AI
{
    public class SearchState : StateBase, IState
    {
        [SerializeField]
        float searchTime = 10f;

        float searchTimer;

        public void StartState()
        {
            Debug.Log("Searching");

            // Check if player is in range or visible 
            // if so, go to chase state
            // else
            // choose a random search waypoint nearby

            searchTimer = searchTime;
        }

        public void EndState()
        {
            // stop searching anim
        }

        public bool IsStateDone()
        {
            // either we can see the player

            // or search time is up

            return false;
        }

        public void UpdateState()
        {
            searchTimer -= Time.deltaTime;
        }
    }
}