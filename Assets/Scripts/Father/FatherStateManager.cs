using System;
using System.Collections;
using UnityEngine;

namespace Father
{
    public class FatherStateManager : MonoBehaviour
    {
        [SerializeField] private PlayerDeathManager _playerDeathManagerCS;

        [SerializeField] private ObjectCurrentRoom _playerCurrentRoomCS;
        [SerializeField] private ObjectCurrentRoom _fatherCurrentRoomCS;

        [SerializeField] private RoomsEnum _playerCurrentRoom;
        [SerializeField] private RoomsEnum _fatherCurrentRoom;
        

        public AudioClip breathingAudioClip;
        public AudioClip respawnAudioClip;
        public AudioSource fatherAudioSource;

        public RoomManager roomManagerCS;
        public Transform respawnTransform;
        public Transform fatherTransform;
        public Transform currentRoomTransform;
        public Animator canvasSmoke;

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

            if (_playerCurrentRoomCS != null) _playerCurrentRoomCS.OnRoomChanged += OnPlayerChangedRoom;
            if (_fatherCurrentRoomCS != null) _fatherCurrentRoomCS.OnRoomChanged += OnFatherChangedRoom;
        }

        private void OnFatherChangedRoom(RoomsEnum room)
        {
            _fatherCurrentRoom = room;
            if (_playerCurrentRoom == _fatherCurrentRoom && _fatherCurrentRoom != RoomsEnum.Couloir)
            {
                _playerDeathManagerCS.ToggleCoroutineSmoke(true);
            }
            else
            {
                _playerDeathManagerCS.ToggleCoroutineSmoke(false);
            }
        }

        private void OnPlayerChangedRoom(RoomsEnum room)
        {
            _playerCurrentRoom = room;
            if (_playerCurrentRoom == _fatherCurrentRoom && _fatherCurrentRoom != RoomsEnum.Couloir)
            {
                _playerDeathManagerCS.ToggleCoroutineSmoke(true);
            }
            else
            {
                _playerDeathManagerCS.ToggleCoroutineSmoke(false);
            }
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

        public void ResetState()
        {
            TransitionToState(respawningState);
        }  
    }
}