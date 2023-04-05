using System.Collections;
using UnityEngine;

namespace Boy
{
    public class RespawningState : BoyState
    {
        private BoyStateManager _manager;
        private float _respawnTime = 3f;

        public RespawningState(BoyStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            Debug.Log("Respawning");
            _manager.navMeshAgentCS.ToggleNavMeshAgent(false);
            _manager.boyTransform.position = _manager.respawnTransform.position;

            _manager.StartCoroutine(CoroutineRespawn());
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            
        }

        private IEnumerator CoroutineRespawn()
        {
            yield return new WaitForSecondsRealtime(_respawnTime);
            _manager.TransitionToState(_manager.lightClosingState);
        }
    }
}