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
            Debug.Log("Search done");
            // stop searching anim
        }

        public bool IsStateDone()
        {
            return searchTimer <= 0;
        }

        public void UpdateState()
        {
            searchTimer -= Time.deltaTime;
        }
    }
}