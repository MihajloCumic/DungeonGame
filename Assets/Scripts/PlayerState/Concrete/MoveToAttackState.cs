using UnityEngine;

public class MoveToAttackState : State
{
    private IDamagable _target;
    private const float attackableDistance = 2f;
    public MoveToAttackState(StateManager stateManager) : base(stateManager)
    {
    }

    public override void CheckForChange()
    {
        if (_target == null || _target.IsDead())
        {
            stateManager.SwitchState(new IdleState(stateManager));
            return;
        }
        if (SwitchToAttack(stateManager.PlayerController.transform.position))
        {
            return;
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

        if (hit.transform.TryGetComponent(out DamagableMinion damagable))
        {
            stateManager.PlayerController.AnimationManager.Run();
            _target = damagable;
            Rotate(playerController.transform, _target.GetPosition());

            playerController.Agent.SetDestination(hit.point);
            return;
        }
        stateManager.SwitchState(new IdleState(stateManager));  
    }

    public override void ExitState()
    {
        return;
    }

    public override void UpdateState()
    {
        return;
    }

    private bool SwitchToAttack(Vector3 playerPosition)
    {
        var distance = Vector3.Distance(playerPosition, _target.GetPosition());
        if (distance <= attackableDistance)
        {
            stateManager.SwitchState(new AttackState(stateManager, _target));
            return true;
        }
        return false;
    }

    private void Rotate(Transform playerTransform, Vector3 targetPosition)
    {
        var direction = targetPosition - playerTransform.position;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        playerTransform.rotation = rotation;
    }
}
