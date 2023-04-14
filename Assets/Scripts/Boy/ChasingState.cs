using UnityEngine;

namespace Boy
{
    public class ChasingState : BoyState
    {
        private BoyStateManager _manager;
        private float _chaseMoveSpeed = 4f;
        private string _boolTrigger = "isChasing";


        public ChasingState(BoyStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            Debug.Log("RUN!");

            _manager.boyAudioSource.clip = _manager.boyChasingClip;
            _manager.boyAudioSource.loop = true;
            _manager.boyAudioSource.Play();

            // Play idle anim;
            _manager.boyAnimator.SetBool(_boolTrigger, true);

            _manager.navMeshAgentCS.ToggleNavMeshAgent(true);
            _manager.navMeshAgentCS.ChangeAgentSpeed(_chaseMoveSpeed);
            
        }

        public override void Execute()
        {
            _manager.navMeshAgentCS.ChangeDestination(_manager.playerTransform.position);
        }

        public override void Exit()
        {
            _manager.boyAudioSource.Stop();
        }
    }
}