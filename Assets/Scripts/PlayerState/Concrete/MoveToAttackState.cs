using UnityEngine;

public class MoveToAttackState : State
{
    private IDamagable _target;
    private readonly Attack _attack;
    public MoveToAttackState(StateManager stateManager, Attack attack) : base(stateManager)
    {
        _attack = attack;
    }

    public override void CheckForChange()
    {
        if (_target == null || _target.IsDead())
        {
            stateManager.SwitchState(stateManager.IdleState);
            return;
        }
        if (IsInRange())
        {
            stateManager.SwitchState(CreateAttackState());
        }
    }

    public override void EnterState()
    {
        var playerController = stateManager.PlayerController;
        Ray ray = playerController.Cemara.ScreenPointToRay(Input.mousePosition);

        bool didHit = RaycastHitUtil.ExludePlayerLayer(ray, out RaycastHit hit);

        if (!didHit)
        {
            stateManager.SwitchState(new IdleState(stateManager));
            return;
        }

        if (hit.transform.TryGetComponent(out IDamagable damagable))
        {
            stateManager.PlayerController.AnimationManager.Run();
            _target = damagable;
            return;
        }
        stateManager.SwitchState(new IdleState(stateManager));
    }

    public override void ExitState()
    {
        _target = null;
    }
    public override void UpdateState()
    {
        var playerController = stateManager.PlayerController;
        Rotate(playerController.transform, _target.GetPosition());
        playerController.Agent.SetDestination(_target.GetPosition());
    }

    private void Rotate(Transform playerTransform, Vector3 targetPosition)
    {
        var direction = targetPosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        playerTransform.rotation = rotation;
    }

    private bool IsInRange()
    {
        var origin = stateManager.PlayerController.transform.position;
        var distance = Vector3.Distance(origin, _target.GetPosition());
        return distance <= _attack.MaxDistance;
    }

    private AttackState CreateAttackState()
    {
        var attackCommand = CommandFactory.CreateAttackCommand(
            _attack,
            stateManager.PlayerController.transform,
            stateManager.PlayerController.AnimationManager,
            _target
        );
        return new AttackState(
            stateManager,
            attackCommand
        );
    }
}
