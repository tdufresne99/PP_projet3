
namespace Mother
{
    public class IdlingState : MotherState
    {
        private MotherStateManager manager;

        public IdlingState(MotherStateManager manager)
        {
            this.manager = manager;
        }
        
        public override void Enter()
        {
            // Enter patrolling state
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