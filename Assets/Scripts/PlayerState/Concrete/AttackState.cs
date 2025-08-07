using System.Collections;
using UnityEngine;

public class AttackState : State
{
    private readonly IDamagable _target;
    private bool _isAttacking = false;
    public AttackState(StateManager stateManager, IDamagable target) : base(stateManager)
    {
        _target = target;
    }

    public override void CheckForChange()
    {
        if (_target == null || _target.IsDead())
        {
            stateManager.SwitchState(new IdleState(stateManager));
        }
    }

    public override void EnterState()
    {
        stateManager.PlayerController.Animator.CrossFade("Idle", 0.1f);
        stateManager.PlayerController.Agent.isStopped = true;
    }

    public override void ExitState()
    {
        stateManager.PlayerController.Agent.isStopped = false;
    }


    public override void UpdateState()
    {
        if (!_isAttacking)
        {
            Attack();
        }
    }

    private void Attack()
    {
        var playerController = stateManager.PlayerController;
        playerController.StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        _isAttacking = true;

        yield return new WaitForSeconds(1f);

        if (IsDead())
        {
            yield break;
        }
    
        _target.TakeDamage(25);
        _isAttacking = false;

    }

    private bool IsDead()
    {
        return _target == null || _target.IsDead();
    }
}
