using UnityEngine;

public class AttackState : State
{
    private readonly ICommand _command;
    public AttackState(StateManager stateManager, ICommand command) : base(stateManager)
    {
        _command = command;
    }

    public override void CheckForChange()
    {
        return;
    }

    public override async void EnterState()
    {
        stateManager.PlayerController.AnimationManager.Idle();
        stateManager.PlayerController.Agent.isStopped = true;

        stateManager.Lock();
        await _command.Execute();
        stateManager.Unlock();
        stateManager.SwitchState(stateManager.IdleState);
    }

    public override void ExitState()
    {
        stateManager.PlayerController.Agent.isStopped = false;
        stateManager.PlayerController.Agent.ResetPath();
    }


    public override void UpdateState()
    {
        return;
    }
}
