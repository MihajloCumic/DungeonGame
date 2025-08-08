using UnityEngine;

public class SpellCastingState : State
{
    private readonly Spell _spell;
    private readonly Transform _casterTransform;
    private readonly AnimationManager _animationManager;
    private readonly GameObject _indicator;
    public SpellCastingState(StateManager stateManager, Spell spell) : base(stateManager)
    {
        _spell = spell;
        _animationManager = stateManager.PlayerController.AnimationManager;
        _casterTransform = stateManager.PlayerController.transform;
        _indicator = Object.Instantiate(_spell.Indicator);
        _indicator.SetActive(false);
    }

    public override void CheckForChange()
    {
        return;
    }

    public override void EnterState()
    {
        return;
    }

    public override void ExitState()
    {
        _indicator.SetActive(false);
    }

    public override async void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            stateManager.Lock();

            var command = CreateCommand();
            await command.Execute();

            stateManager.Unlock();
            stateManager.SwitchState(stateManager.IdleState);
            return;
        }
        DrawIndicator();
    }
    private ICommand CreateCommand()
    {
        return CommandFactory.CreateSpellCommand(
            _spell,
            _casterTransform,
            _animationManager
        );
    }

    private void DrawIndicator()
    {
        var mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool didHit = RaycastHitUtil.ExludePlayerLayer(mouseRay, out RaycastHit hit);
        if (!didHit)
        {
            return;
        }

        Rotate(hit.point);
        _spell.DrawIndicator(_casterTransform.position, hit, _indicator);
    }

    private void Rotate(Vector3 mousePosition)
    {
        var direction = mousePosition - _casterTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        _casterTransform.rotation = rotation;
    }
}
