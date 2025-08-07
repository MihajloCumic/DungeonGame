using System.Threading.Tasks;
using UnityEngine;

public class FireSpellCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly FireSpell _fireSpell;
    private readonly AnimationManager _animatrionManager;

    public FireSpellCommand(Transform casterTransform, FireSpell fireSpell, AnimationManager animationManager)
    {
        _casterTransform = casterTransform;
        _fireSpell = fireSpell;
        _animatrionManager = animationManager;
    }

    public async Task Execute()
    {
        var origin = _casterTransform.position;
        var direction = _casterTransform.forward;
        var maxDistance = _fireSpell.MaxDistance;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                float duration = _animatrionManager.Cast();
                await Awaitable.WaitForSecondsAsync(duration);
                damagable.TakeDamage(_fireSpell.BaseDamage);
                _animatrionManager.Idle();
            }

        }
    }
}
