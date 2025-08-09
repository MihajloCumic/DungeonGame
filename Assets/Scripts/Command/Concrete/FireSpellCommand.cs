using System;
using System.Threading.Tasks;
using UnityEngine;

public class FireSpellCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly FireSpell _fireSpell;
    private readonly Func<float> _animationFunc;

    public FireSpellCommand(Transform casterTransform, FireSpell fireSpell, Func<float> animationFunc)
    {
        _casterTransform = casterTransform;
        _fireSpell = fireSpell;
        _animationFunc = animationFunc;
    }

    public async Task Execute()
    {
        float duration = _animationFunc();
        await Awaitable.WaitForSecondsAsync(duration);

        var origin = _casterTransform.position;
        var direction = _casterTransform.forward;
        var maxDistance = _fireSpell.MaxDistance;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_fireSpell.BaseDamage);
            }

        }
    }

    
}
