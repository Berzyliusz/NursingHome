using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NursingHome.AI
{
    public class AI : MonoBehaviour
    {
        IState patrolState;
        IState idleState;

        IState currentState;

        bool wasPatrolling; // This is utterly retarded;

        void Awake()
        {
            idleState= GetComponent<IdleState>();
            patrolState= GetComponent<PatrolState>();
        }

        void Start()
        {
            SwitchState(idleState);
        }

        void SwitchState(IState newState)
        {
            if (currentState != null)
            {
                currentState.EndState();
            }

            currentState = newState;
            currentState.StartState();
        }

        void Update()
        {
            if (currentState == null)
                return;

            currentState.UpdateState();

            if (currentState.IsStateDone())
            {
                IState newState = ChooseNewState();
                SwitchState(newState);
            }
        }

        IState ChooseNewState()
        {
            IState newState;
            if (wasPatrolling)
                newState = idleState;
            else
                newState = patrolState;

            wasPatrolling = !wasPatrolling;
            return newState;
        }
    }
}