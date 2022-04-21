
namespace StateMachine
{
    public interface IStateMachineContext
    {
        public IState CurrentState
        {
            get;
            set;
        }
    }

}