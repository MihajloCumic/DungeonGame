using UnityEngine;

public class SpellCastingState : State
{
    private readonly Spell _spell;
    private readonly Transform _casterTransform;
    private readonly AnimationManager _animationManager;
    private readonly GameObject _indicator;
    private readonly Camera _mainCamera;
    public SpellCastingState(StateManager stateManager, Spell spell) : base(stateManager)
    {
        _spell = spell;
        _animationManager = stateManager.PlayerController.AnimationManager;
        _casterTransform = stateManager.PlayerController.transform;
        _indicator = Object.Instantiate(_spell.Indicator);
        _indicator.SetActive(false);
        _mainCamera = stateManager.PlayerController.Cemara;
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
        Object.Destroy(_indicator);
    }

    public override async void UpdateState()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!DidHit(out RaycastHit hit))
            {
                stateManager.SwitchState(stateManager.IdleState);
                return;
            }
            stateManager.Lock();

            var command = CreateCommand(hit.point);
            await command.Execute();

            stateManager.Unlock();
            stateManager.SwitchState(stateManager.IdleState);
            return;
        }
        DrawIndicator();
    }
    private ICommand CreateCommand(Vector3 mouseHitPosition)
    {
        return CommandFactory.CreateSpellCommand(
            _spell,
            _casterTransform,
            _animationManager.Cast,
            mouseHitPosition
        );
    }

    private void DrawIndicator()
    {
        if (!DidHit(out RaycastHit hit))
        {
            return;
        }

        Rotate(hit.point);
        _spell.DrawIndicator(_casterTransform.position, hit, _indicator);
    }

    private void Rotate(Vector3 mousePosition)
    {
        var direction = (mousePosition - _casterTransform.position).normalized;
        direction.y = 0.1f;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        _casterTransform.rotation = rotation;
    }

    public static SpellCastingState Create(StateManager stateManager, Spell spell)
    {
        return new SpellCastingState(
            stateManager,
            spell
        );
    }

    private bool DidHit(out RaycastHit hit)
    {
        var mouseRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        return RaycastHitUtil.ExludePlayerLayer(mouseRay, out hit);
        
    }
}
