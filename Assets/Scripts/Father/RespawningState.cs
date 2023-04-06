using System.Collections;
using UnityEngine;

namespace Father
{
    public class RespawningState : FatherState
    {
        private FatherStateManager _manager;
        private float _respawnTime = 5f;
        private Coroutine _coroutineRespawn;

        public RespawningState(FatherStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            Debug.Log("Respawning");

            // Play idle anim;
            _manager.GetComponent<MeshRenderer>().material = _manager.respawnMat;

            _manager.fatherTransform.position = _manager.respawnTransform.position;

            _coroutineRespawn = _manager.StartCoroutine(CoroutineRespawn());
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            _manager.StopCoroutine(_coroutineRespawn);
        }

        private IEnumerator CoroutineRespawn()
        {
            yield return new WaitForSecondsRealtime(_respawnTime);
            _manager.TransitionToState(_manager.roomChoosingState);
        }
    }
}