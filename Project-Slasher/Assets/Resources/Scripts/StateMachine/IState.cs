
namespace StateMachine
{
    public interface IState
    {
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract bool TrySwitchState(IState newState);

        public abstract void FixedUpdateState();

        public abstract void ExitState();

        public abstract bool IsStateSwitchable();

        public abstract void CheckSwitchState();
    }

}