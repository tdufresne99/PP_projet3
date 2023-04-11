namespace Girl
{
    public class IdlingState : GirlState
    {
        private GirlStateManager _manager;
        private string idleTrigger = "idle";
        public IdlingState(GirlStateManager manager)
        {
            this._manager = manager;
        }
        public override void Enter()
        {
            _manager.girlAnimator.SetTrigger(idleTrigger);
        }

        public override void Execute()
        {
            
        }

        public override void Exit()
        {
            
        }
    }
}