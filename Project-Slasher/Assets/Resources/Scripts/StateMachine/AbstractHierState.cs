
using UnityEngine;

namespace StateMachine
{
    public abstract class AbstractHierState : StateMachine.IState
    {
        private AbstractHierState currentSuperState;
        private AbstractHierState currentSubState;

        public AbstractHierState CurrentSubState => currentSubState;

        private IStateMachineContext stateMachine;
        protected IStateMachineContext StateMachine
        {
            get => stateMachine;
        }

        public AbstractHierState(IStateMachineContext stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public abstract void EnterState();

        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }

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

        public void FixedUpdateStates()
        {
            FixedUpdateState();
            if (currentSubState != null)
            {
                currentSubState.FixedUpdateStates();
            }
        }

        protected void SwitchState(AbstractHierState newState)
        {
            ExitState();
            //Only assign new current root state if current state is root
            if (currentSuperState == null)
            {
                stateMachine.CurrentState = newState;
            }
            else
            {
                currentSuperState.SetSubState(newState);
            }
            newState.EnterState();
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
