using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;

public class FollowAndAttackState
{
    private readonly MinionAttack _minionAttack;
    private readonly Transform _transform;
    private readonly IDamagable _player;
    private readonly NavMeshAgent _agent;
    private readonly AnimationManager _animationManager;
    private bool _lockState = false;
    private float _lastAttackTime = -Mathf.Infinity;

    public FollowAndAttackState(
        MinionAttack minionAttack,
        Transform transform,
        IDamagable player,
        NavMeshAgent agent,
        AnimationManager animationManager
    )
    {
        _minionAttack = minionAttack;
        _transform = transform;
        _player = player;
        _agent = agent;
        _animationManager = animationManager;
    }

    public void EnterState()
    {
        _animationManager.Run();
    }


    public void UpdateState()
    {
        if (!_transform.gameObject.activeInHierarchy) return;
        if (_lockState) return;
        TryToAttack();
    }

    public async void TryToAttack()
    {
        Rotate();
        if (IsInRange() && IsCoolDownUp())
        {
            _agent.ResetPath();
            await Attack();
            return;
        }
        _agent.SetDestination(_player.GetPosition());
    }

    private async Task Attack()
    {
        _lockState = true;
        var command = CreateAttackCommand();
        await command.Execute();
        _lastAttackTime = Time.time;
        _lockState = false;
        _animationManager.Run();
    }

    private ICommand CreateAttackCommand()
    {
        return CommandFactory.CreateAttackCommand(
            _minionAttack,
            _transform,
            _animationManager.MinionAttack,
            _player
        );
    }

    private bool IsCoolDownUp()
    {
        var attackCooldown = _minionAttack.Cooldown;
        return Time.time >= _lastAttackTime - attackCooldown;
    }

    private bool IsInRange()
    {
        var range = _minionAttack.MaxDistance;
        var position = _transform.position;
        var playerPosition = _player.GetPosition();
        var distance = Vector3.Distance(position, playerPosition);
        return distance <= range;
    }
    private void Rotate()
    {
        var position = _transform.position;
        var playerPosition = _player.GetPosition();
        var direction = position - playerPosition;
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        _transform.rotation = rotation;
    }
}
