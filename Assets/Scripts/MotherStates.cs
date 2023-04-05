using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState
{
    Patrolling,
    Spotting,
    Chasing
}
namespace Temp
{
    public class MotherStates : MonoBehaviour
    {
        [SerializeField] private Transform _destination;
        private NavMeshAgent _agent;
        private AIState currentState = AIState.Patrolling;

        void Start()
        {
            currentState = AIState.Patrolling;

            _agent = GetComponent<NavMeshAgent>();
            _agent.destination = _destination.position;
        }

        void Update()
        {
            // Check the current state and perform the appropriate behavior
            switch (currentState)
            {
                case AIState.Patrolling:
                    // Do patrolling behavior
                    break;
                case AIState.Spotting:
                    // Do spotting behavior
                    break;
                case AIState.Chasing:
                    // Do chasing behavior
                    break;
            }
        }

        // Transition to a new state
        private void ChangeState(AIState newState)
        {
            // Exit the current state
            switch (currentState)
            {
                case AIState.Patrolling:
                    // Exit patrolling state
                    break;
                case AIState.Spotting:
                    // Exit spotting state
                    break;
                case AIState.Chasing:
                    // Exit chasing state
                    break;
            }

            // Enter the new state
            switch (newState)
            {
                case AIState.Patrolling:
                    // Enter patrolling state
                    break;
                case AIState.Spotting:
                    // Enter spotting state
                    break;
                case AIState.Chasing:
                    // Enter chasing state
                    break;
            }

            // Set the new state
            currentState = newState;
        }
    }
}

