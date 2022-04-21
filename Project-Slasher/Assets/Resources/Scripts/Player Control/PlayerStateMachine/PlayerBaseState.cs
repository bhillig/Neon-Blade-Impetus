
public abstract class PlayerBaseState : StateMachine.AbstractHierState
{
    private PlayerStateFactory factory;
    private PlayerBaseState currentSuperState;
    private PlayerBaseState currentSubState;

    protected PlayerStateFactory Factory
    {
        get => Factory;
    }

    public PlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory) : base(context)
    {
        this.factory = factory;
    }
}
