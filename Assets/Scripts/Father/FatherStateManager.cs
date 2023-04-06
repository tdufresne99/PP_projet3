using UnityEngine;

namespace Father
{
    public class FatherStateManager : MonoBehaviour
    {
        public Material smokeMat;
        public Material roomChooseMat;
        public Material respawnMat;

        public RoomManager roomManagerCS;
        public Transform respawnTransform;
        public Transform fatherTransform;
        public Transform playerTransform;
        public Transform currentRoomTransform;
        // public LayerMask wallsLayerMask;


        public FatherState currentState;

        public RespawningState respawningState;
        public RoomChoosingState roomChoosingState;
        public SmokingState smokingState;

        private void Start()
        {
            respawningState = new RespawningState(this);
            roomChoosingState = new RoomChoosingState(this);
            smokingState = new SmokingState(this);

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

        public void TransitionToState(FatherState newState)
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