using System.Collections;
using UnityEngine;

namespace Boy
{
    public class RespawningState : BoyState
    {
        private BoyStateManager _manager;
        private float _respawnTime = 3f;
        private Coroutine _coroutineRespawn;

        public RespawningState(BoyStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            Debug.Log("Respawning");

            // Play idle anim;

            _manager.navMeshAgentCS.ToggleNavMeshAgent(false);
            _manager.boyTransform.position = _manager.respawnTransform.position;

            _coroutineRespawn = _manager.StartCoroutine(CoroutineRespawn());
        }

        public override void Execute()
        {

        }

        public override void Exit()
        {
            if (_coroutineRespawn != null) _manager.StopCoroutine(_coroutineRespawn);
        }

        private IEnumerator CoroutineRespawn()
        {
            yield return new WaitForSecondsRealtime(_respawnTime);
            _manager.TransitionToState(_manager.lightClosingState);
        }
    }
}