using UnityEngine;

public class StateManager
{
    private State currState;
    public PlayerController PlayerController { get; private set; }
    private bool _stateManagerLock = false;
    private readonly Spell _firstSpell;
    private readonly SpellCastingState _spellCastingState;

    public StateManager(PlayerController playerController)
    {
        PlayerController = playerController;
        currState = new IdleState(this);
        _firstSpell = playerController.SpellSet.FirstSpell;
        _spellCastingState = new SpellCastingState(this, _firstSpell);
        
    }

    public void SwitchState(State nextState)
    {
        currState.ExitState();
        currState = nextState;
        currState.EnterState();
    }

    public void Update()
    {
        if (_stateManagerLock) return;
        currState.UpdateState();
        currState.CheckForChange();
        CheckForInputChanges();
    }

    public void Lock()
    {
        _stateManagerLock = true;
    }
    public void Unlock()
    {
        _stateManagerLock = false;
    }

    private void CheckForInputChanges()
    {
        if (Input.GetMouseButton(1))
        {
            SwitchState(new MoveState(this));
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwitchState(_spellCastingState);
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            SwitchState(new MoveToAttackState(this));
        }
    }

    
}
