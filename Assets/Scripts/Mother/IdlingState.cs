
namespace Mother
{
    public class IdlingState : MotherState
    {
        private MotherStateManager _manager;

        public IdlingState(MotherStateManager manager)
        {
            this._manager = manager;
        }

        public override void Enter()
        {
            // Enter patrolling state

            // Play idle anim;

            // Play idle sound;
        }

        public override void Execute()
        {
            // Do patrolling behavior
        }

        public override void Exit()
        {
            // Exit patrolling state
        }
    }
}