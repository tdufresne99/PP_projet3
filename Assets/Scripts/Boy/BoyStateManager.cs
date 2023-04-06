using UnityEngine;

namespace Boy
{
    public class BoyStateManager : MonoBehaviour
    {
        public Material chaseMat;
        public Material idleMat;
        public Material lightCloseMat;
        public Material respawnMat;

        public RoomManager roomManagerCS;
        public NavMeshAgentManager navMeshAgentCS;
        public Transform respawnTransform;
        public Transform boyTransform;
        public Transform playerTransform;
        public LayerMask wallsLayerMask;

        public BoyState currentState;

        public RespawningState respawningState;
        public IdlingState idlingState;
        public LightClosingState lightClosingState;
        public ChasingState chasingState;

        private void Start()
        {
            navMeshAgentCS.ToggleNavMeshAgent(false);

            respawningState = new RespawningState(this);
            idlingState = new IdlingState(this);
            lightClosingState = new LightClosingState(this);
            chasingState = new ChasingState(this);

            // Start in patrolling state
            TransitionToState(respawningState);
        }

        void Update()
        {
            if (currentState == null)
            {
                // Transition to patrolling state by default
                TransitionToState(respawningState);
            }

            currentState.Execute();
        }

        public void TransitionToState(BoyState newState)
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