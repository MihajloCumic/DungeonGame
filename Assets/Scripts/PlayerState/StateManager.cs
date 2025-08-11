using UnityEngine;

public class StateManager
{
    private State currState;
    public PlayerController PlayerController { get; private set; }
    private bool _stateManagerLock = false;
    private readonly IdleState _idleState;
    private readonly MoveToAttackState _moveToAttackState;
    private readonly MoveState _moveState;

    public IdleState IdleState => _idleState;
    public MoveToAttackState MoveToAttackState => _moveToAttackState;
    public MoveState MoveState => _moveState;


    public StateManager(PlayerController playerController)
    {
        PlayerController = playerController;
        _idleState = new IdleState(this);
        currState = _idleState;
        var attack = playerController.PlayerStats.MeleAttack;
        _moveToAttackState = new MoveToAttackState(this, attack);
        _moveState = new MoveState(this);
        SetUpCooldowns(playerController.SpellSet);
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
        if (Input.GetMouseButtonDown(1))
        {
            SwitchState(_moveState);
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            var qSpell = PlayerController.SpellSet.FirstSpell;
            SwitchState(SpellCastingState.Create(this, qSpell));
            return;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            var wSpell = PlayerController.SpellSet.SecondSpell;
            SwitchState(SpellCastingState.Create(this, wSpell));
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            var eSpell = PlayerController.SpellSet.ThirdSpell;
            SwitchState(SpellCastingState.Create(this, eSpell));
        }
    }


    private void SetUpCooldowns(SpellSet spellSet)
    {
        SpellCastingState.SetUpCooldowns(new Spell[]{
            spellSet.FirstSpell,
            spellSet.SecondSpell,
            spellSet.ThirdSpell
        });
    }
}
