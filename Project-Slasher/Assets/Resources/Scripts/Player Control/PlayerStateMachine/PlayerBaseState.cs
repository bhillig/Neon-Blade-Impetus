
public abstract class PlayerBaseState 
{
    private bool isRootState = false;
    private PlayerStateMachine context;
    private PlayerStateFactory factory;
    private PlayerBaseState currentSuperState;
    private PlayerBaseState currentSubState;

    protected bool IsRootState
    {
        set => isRootState = value;
    }

    protected PlayerStateMachine Context
    {
        get => context;
    }

    protected PlayerStateFactory Factory
    {
        get => Factory;
    }

    public PlayerBaseState(PlayerStateMachine context, PlayerStateFactory factory)
    {
        this.context = context;
        this.factory = factory;
    }
    public abstract void EnterState();

    public abstract void UpdateState();

    public abstract void ExitState();

    public abstract void CheckSwitchStates();

    public abstract void InitializeSubState();

    public void UpdateStates()
    {
        UpdateState();
        if(currentSubState != null)
        {
            currentSubState.UpdateStates();
        }
    }

    protected void SwitchState(PlayerBaseState newState)
    {
        ExitState();
        newState.EnterState();
        if(isRootState)
        {
            context.CurrentState = newState;
        }
        else if(currentSuperState != null)
        {
            currentSuperState.SetSubState(newState);
        }
    }

    protected void SetSuperState(PlayerBaseState newSuperState)
    {
        currentSuperState = newSuperState;
    }

    protected void SetSubState(PlayerBaseState newSubState)
    {
        currentSubState = newSubState;
        newSubState.SetSuperState(this);
    }
}
