
using UnityEngine;

public class IdleState : State
{
    public IdleState(StateManager stateManager) : base(stateManager)
    {
    }

    public override void ExitState()
    {
        return;
    }

    public override void CheckForChange()
    {
        return;
    }

    public override void EnterState()
    {
        stateManager.PlayerController.AnimationManager.Idle();
    }
    

    public override void UpdateState()
    {
        return;
    }

    
}
