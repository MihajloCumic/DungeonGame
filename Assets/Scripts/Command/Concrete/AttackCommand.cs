using System.Threading.Tasks;
using UnityEngine;

public class AttackCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly Attack _attack;
    private readonly AnimationManager _animationManager;
    private readonly IDamagable _target;

    public AttackCommand(
        Transform casterTransform,
        Attack attack,
        AnimationManager animationManager,
        IDamagable target)
    {
        _casterTransform = casterTransform;
        _attack = attack;
        _animationManager = animationManager;
        _target = target;
    }

    public async Task Execute()
    {
        var origin = _casterTransform.position;
        var maxDistance = _attack.MaxDistance;

        var distance = Vector3.Distance(origin, _target.GetPosition());
        if (distance > maxDistance)
        {
            return;
        }

        float duration = _animationManager.Mele();
        await Awaitable.WaitForSecondsAsync(duration);

        _target.TakeDamage(_attack.BaseDamage);
    }
}
