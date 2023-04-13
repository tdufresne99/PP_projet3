using UnityEngine;

namespace Girl
{
    public class IdlingState : GirlState
    {
        private GirlStateManager _manager;
        private string walkTrigger = "isWalking";
        public IdlingState(GirlStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            // Play Idle Anim
            _manager.girlAnimator.SetBool(walkTrigger, false);
            Debug.Log("girl is idle");
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}