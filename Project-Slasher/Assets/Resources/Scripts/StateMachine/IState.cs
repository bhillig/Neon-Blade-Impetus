
namespace StateMachine
{
    public interface IState
    {
        public abstract void EnterState();

        public abstract void UpdateState();

        public abstract void ExitState();

        public abstract void CheckSwitchStates();
    }

}