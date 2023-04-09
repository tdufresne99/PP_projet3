using UnityEngine;

namespace Mother
{
    public class MotherStateManager : MonoBehaviour
    {
        public Material patrolMat;
        public Material spotMat;
        public Material chaseMat;
        public Material idleMat;

        public NavMeshAgentManager navMeshDestinationCS;
        public RoomManager roomManager;
        public Transform playerTransform;
        public Transform motherTransform;
        public LayerMask wallsLayerMask;

        public MotherState currentState;

        public IdlingState idlingState;
        public PatrollingState patrollingState;
        public SpottingState spottingState;
        public ChasingState chasingState;
        

        // Initialize state variables
        private void Start()
        {
            motherTransform = transform;

            idlingState = new IdlingState(this);
            patrollingState = new PatrollingState(this);
            spottingState = new SpottingState(this);
            chasingState = new ChasingState(this);

            // Start in patrolling state
            TransitionToState(patrollingState);
        }
        void Update()
        {
            if (currentState == null)
            {
                // Transition to patrolling state by default
                TransitionToState(patrollingState);
            }

            currentState.Execute();
        }

        public void TransitionToState(MotherState newState)
        {
            if (currentState != null)
            {
                currentState.Exit();
            }

            currentState = newState;
            currentState.Enter();
        }

        public Vector3 GetRandomDestination()
        {
            int randIndex = Random.Range(0, roomManager.roomTransforms.Length);
            return roomManager.roomTransforms[randIndex].position;
        }
    }
}
