

namespace StateMachine
{
    public abstract class AbstractHierState : StateMachine.IState
    {
        private AbstractHierState currentSuperState;
        private AbstractHierState currentSubState;
        private IStateMachineContext abstractContext;
        protected IStateMachineContext AbstractContext
        {
            get => abstractContext;
        }

        public AbstractHierState(IStateMachineContext context)
        {
            this.abstractContext = context;
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
                abstractContext.CurrentState = newState;
            }
            else
            {
                currentSuperState.SetSubState(newState);
            }
        }
        /// <summary>
        /// Switches or initializes this substate to new state
        /// </summary>
        protected void SwitchSubState(AbstractHierState newState)
        {
            if(currentSubState != null)
            {
                currentSubState.SwitchState(newState);
            }
            else
            {
                SetSubState(newState);
                newState.EnterState();
            }
        }

        private void SetSuperState(AbstractHierState newSuperState)
        {
            currentSuperState = newSuperState;
        }

        private void SetSubState(AbstractHierState newSubState)
        {
            currentSubState = newSubState;
            newSubState.SetSuperState(this);
        }
    }

}
