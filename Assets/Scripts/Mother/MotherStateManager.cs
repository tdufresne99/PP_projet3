using UnityEngine;

namespace Mother
{
    public class MotherStateManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _motherRespawnTransforms;

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

        public Animator motherAnimator;
        public AudioSource motherAudioSource;
        public AudioClip motherChase;
        public AudioClip motherIdle;
        public AudioClip motherSpot;

        public IdlingState idlingState;
        public PatrollingState patrollingState;
        public SpottingState spottingState;
        public ChasingState chasingState;


        // Initialize state variables
        private void Awake()
        {
            motherTransform = transform;

            idlingState = new IdlingState(this);
            patrollingState = new PatrollingState(this);
            spottingState = new SpottingState(this);
            chasingState = new ChasingState(this);

            // Start in patrolling state
            ResetState();
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

        public void ResetState()
        {
            motherTransform.position = _motherRespawnTransforms[Random.Range(0, _motherRespawnTransforms.Length)].position;
            TransitionToState(idlingState);
        }
    }
}

