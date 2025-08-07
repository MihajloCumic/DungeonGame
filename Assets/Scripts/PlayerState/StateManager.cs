using UnityEngine;

public class StateManager
{
    private State currState;
    public PlayerController PlayerController { get; private set; }

    public StateManager(PlayerController playerController)
    {
        PlayerController = playerController;
        currState = new IdleState(this);
    }

    public void SwitchState(State nextState)
    {
        currState.ExitState();
        currState = nextState;
        currState.EnterState();
    }

    public void Update()
    {
        currState.UpdateState();
        currState.CheckForChange();
        CheckForInputChanges();
    }

    private void CheckForInputChanges()
    {
        if (Input.GetMouseButton(1))
        {
            SwitchState(new MoveState(this));
            return;
        }
        // if (Input.GetMouseButtonUp(0))
        // {
        //     SwitchState(new MoveToAttackState(this));
        // }
    }
    
}
