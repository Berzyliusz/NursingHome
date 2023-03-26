using UnityEngine;

namespace NursingHome.AI
{
    public class AI : MonoBehaviour
    {
        IState patrolState;
        IState idleState;

        IState currentState;

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


        bool wasPatrolling; // This is utterly retarded;
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