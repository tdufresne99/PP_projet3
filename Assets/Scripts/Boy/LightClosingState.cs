using UnityEngine;

namespace Boy
{
    public class LightClosingState : BoyState
    {
        private Transform _respawnDestination;
        private RoomLight _lightToTurnOff;

        private BoyStateManager _manager;

        public LightClosingState(BoyStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            Debug.Log("Closing the light");
            _respawnDestination = _manager.roomManagerCS.FindRandomRoomWithLightsOn(true);
            if(_respawnDestination != null) 
            {
                _lightToTurnOff = _manager.roomManagerCS.GetLightToTurnOff(_respawnDestination);
                _manager.boyTransform.position = _respawnDestination.position;
            }
            else
            {
                _manager.boyTransform.position = _manager.roomManagerCS.mapCenterTransform.position;
                _manager.TransitionToState(_manager.chasingState);
            }
            
            if(_lightToTurnOff != null) 
            {
                _lightToTurnOff.LightIsOn = false;

                _manager.TransitionToState(_manager.idlingState);
            }
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}