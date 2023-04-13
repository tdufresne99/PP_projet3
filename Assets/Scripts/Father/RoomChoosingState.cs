using System.Collections;
using UnityEngine;

namespace Father
{
    public class RoomChoosingState : FatherState
    {
        private FatherStateManager _manager;
        private Transform _newRoomTranform;
        private Coroutine _choosingRoom;
        private float _choosingRoomWaitTime = 5f;

        public RoomChoosingState(FatherStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            ChoosingRoom();
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            if(_choosingRoom != null) _manager.StopCoroutine(_choosingRoom);
        }

        private void ChoosingRoom()
        {
            Debug.Log("Choosing a room to smoke...");

            _newRoomTranform = _manager.roomManagerCS.FindRandomRoomWithLightsOn(false);
            if (_newRoomTranform != null)
            {
                _manager.currentRoomTransform = _newRoomTranform.parent.transform;
                _manager.fatherTransform.position = _newRoomTranform.position;
                _manager.TransitionToState(_manager.smokingState);
            }
            else
            {
                _choosingRoom = _manager.StartCoroutine(CoroutineChoosingRoom());
            }
        }

        private IEnumerator CoroutineChoosingRoom()
        {
            yield return new WaitForSecondsRealtime(_choosingRoomWaitTime);
            ChoosingRoom();
        }

    }
}