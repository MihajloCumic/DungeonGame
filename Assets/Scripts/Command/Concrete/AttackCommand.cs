using System;
using System.Threading.Tasks;
using UnityEngine;

public class AttackCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly Attack _attack;
    private readonly Func<float> _animationFunc;
    private readonly IDamagable _target;

    public AttackCommand(
        Transform casterTransform,
        Attack attack,
        Func<float> animationFunc,
        IDamagable target)
    {
        _casterTransform = casterTransform;
        _attack = attack;
        _animationFunc = animationFunc;
        _target = target;
    }

    public async Task Execute()
    {
        if (!_casterTransform.gameObject.activeInHierarchy) return;
        float duration = _animationFunc();
        await Awaitable.WaitForSecondsAsync(duration);

        var origin = _casterTransform.position;
        var maxDistance = _attack.MaxDistance;

        var distance = Vector3.Distance(origin, _target.GetPosition());
        if (distance > maxDistance)
        {
            return;
        }

        _target.TakeDamage(_attack.BaseDamage);
    }
}
