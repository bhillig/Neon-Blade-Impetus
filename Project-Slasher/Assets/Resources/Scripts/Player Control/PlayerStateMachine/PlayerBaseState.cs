
public abstract class PlayerBaseState : StateMachine.AbstractHierState
{
    private PlayerStateFactory factory;
    private PlayerBaseState currentSuperState;
    private PlayerBaseState currentSubState;

    protected PlayerStateMachine Context
    {
        get => (PlayerStateMachine)AbstractContext;
    }

    protected PlayerStateFactory Factory
    {
        get => factory;
    }

    public PlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory) : base(context)
    {
        this.factory = factory;
    }
}
