
public abstract class State
{
    protected StateManager stateManager;

    public State(StateManager stateManager)
    {
        this.stateManager = stateManager;
    }

    public abstract void UpdateState();
    public abstract void EnterState();
    public abstract void CheckForChange();
    public abstract void ExitState();
}
