

namespace StateMachine
{
    public abstract class AbstractHierState : StateMachine.IState
    {
        private AbstractHierState currentSuperState;
        private AbstractHierState currentSubState;
        private IStateMachineContext context;
        protected IStateMachineContext Context
        {
            get => context;
        }

        public AbstractHierState(IStateMachineContext context)
        {
            this.context = context;
        }

        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchStates();

        public abstract void InitializeSubState();

        public void UpdateStates()
        {
            UpdateState();
            if (currentSubState != null)
            {
                currentSubState.UpdateStates();
            }
        }

        protected void SwitchState(AbstractHierState newState)
        {
            ExitState();
            newState.EnterState();
            //Only assign new current root state if current state is root
            if (currentSuperState == null)
            {
                context.CurrentState = newState;
            }
            else
            {
                currentSuperState.SetSubState(newState);
            }
        }

        protected void SetSuperState(AbstractHierState newSuperState)
        {
            currentSuperState = newSuperState;
        }

        protected void SetSubState(AbstractHierState newSubState)
        {
            currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }

}
