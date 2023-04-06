using UnityEngine;

namespace Boy
{
    public class ChasingState : BoyState
    {
        private BoyStateManager _manager;
        private float _chaseMoveSpeed = 4f;

        public ChasingState(BoyStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            Debug.Log("RUN!");

            // Play idle anim;
            _manager.GetComponent<MeshRenderer>().material = _manager.chaseMat;

            _manager.navMeshAgentCS.ToggleNavMeshAgent(true);
            _manager.navMeshAgentCS.ChangeAgentSpeed(_chaseMoveSpeed);
            
        }

        public override void Execute()
        {
            _manager.navMeshAgentCS.ChangeDestination(_manager.playerTransform.position);
        }

        public override void Exit()
        {
            
        }
    }
}