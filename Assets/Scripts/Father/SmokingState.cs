using System.Collections;
using UnityEngine;

namespace Father
{
    public class SmokingState : FatherState
    {
        private FatherStateManager _manager;
        private RoomSmoke _smokeToActivate;
        private Coroutine _coroutineSmoking;
        private RoomSmoke _currentRoomSmoke;
        private float _smokingTime = 22f;

        public SmokingState(FatherStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            _currentRoomSmoke = _manager.currentRoomTransform.gameObject.GetComponent<RoomSmoke>();
            if(_currentRoomSmoke != null)
            {
                Debug.Log("Smoking the room...");
                _currentRoomSmoke.SmokeIsOn = true;
            }
            else 
            {
                Debug.LogWarning("No smoke found in current room...");
            }
            _coroutineSmoking = _manager.StartCoroutine(CoroutineSmoking());
            _manager.fatherAudioSource.clip = _manager.respawnAudioClip;
            _manager.fatherAudioSource.loop = true;
            _manager.fatherAudioSource.Play();
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            _currentRoomSmoke.SmokeIsOn = false;
            if(_coroutineSmoking != null) _manager.StopCoroutine(_coroutineSmoking);

            _manager.canvasSmoke.SetBool("smokeIsOn", false);
        }

        private IEnumerator CoroutineSmoking()
        {
            yield return new WaitForSecondsRealtime(_smokingTime);
            _manager.TransitionToState(_manager.respawningState);
        }
    }
}