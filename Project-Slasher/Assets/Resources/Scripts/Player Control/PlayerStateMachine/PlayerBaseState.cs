
public abstract class PlayerBaseState : StateMachine.AbstractHierState
{
    private PlayerStateFactory factory;

    private PlayerController context;
    protected PlayerController Context
    {
        get => context;
    }

    protected PlayerStateFactory Factory
    {
        get => factory;
    }

    public PlayerBaseState(PlayerStateMachine stateMachine, PlayerStateFactory factory) : base(stateMachine)
    {
        this.factory = factory;
        this.context = stateMachine.Context;
    }
}
