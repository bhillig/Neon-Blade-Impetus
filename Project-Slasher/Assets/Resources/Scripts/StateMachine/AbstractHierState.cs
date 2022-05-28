
using UnityEngine;

namespace StateMachine
{
    public abstract class AbstractHierState : StateMachine.IState
    {
        private IStateMachineContext stateMachine;
        protected IStateMachineContext StateMachine
        {
            get => stateMachine;
        }

        public AbstractHierState(IStateMachineContext stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        /// <summary>
        /// Logic for determining if the state can be switched to or not
        /// </summary>
        /// <returns></returns>
        public virtual bool IsStateSwitchable()
        {
            return true;
        }

        /// <summary>
        /// <para>Attempt to switch to a new state</para>
        /// <para>Will fail if the new state fails IsStateSwitchable()</para>
        /// </summary>
        /// <param name="newState"></param>
        public bool TrySwitchState(IState newState)
        {
            if (!newState.IsStateSwitchable() || stateMachine.CurrentState != this)
                return false;
            ExitState();
            stateMachine.CurrentState = newState;
            newState.EnterState();
            return true;
        }

        public abstract void EnterState();
        public abstract void ExitState();
        public abstract void UpdateState();
        public abstract void FixedUpdateState();
        public abstract void CheckSwitchState();
    }

}
