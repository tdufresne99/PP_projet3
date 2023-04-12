using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Girl
{
    public class GirlStateManager : MonoBehaviour
    {
        public NavMeshAgentManager girlNavMeshAgentManager;
        public Transform girlTransform;
        public Transform playerTransform;
        public LayerMask wallsLayerMask;
        public Animator girlAnimator;
        public GirlState currentState;
        public IdlingState idlingState;
        public WalkingState walkingState;
        private void Awake()
        {
            girlTransform = transform;

            idlingState = new IdlingState(this);
            walkingState = new WalkingState(this);
        }

        void Update()
        {
            if (currentState == null)
            {
                // Transition to patrolling state by default
                TransitionToState(idlingState);
            }

            currentState.Execute();
        }

        public void TransitionToState(GirlState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }
    }
}