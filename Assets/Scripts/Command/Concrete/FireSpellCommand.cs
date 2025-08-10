using System;
using System.Runtime.CompilerServices;
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
        PathEffect();
        float duration = _animationFunc();
        await Awaitable.WaitForSecondsAsync(duration);

        var origin = _casterTransform.position;
        var direction = _casterTransform.forward;
        var maxDistance = _fireSpell.MaxDistance;

        if (Physics.Raycast(origin, direction, out RaycastHit hit, maxDistance))
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                HitEffect(damagable);
                damagable.TakeDamage(_fireSpell.BaseDamage);
                SceneRules.Instance.CheckRules(_casterTransform.GetComponent<IDamagable>(), damagable, _fireSpell, null);
            }

        }
    }
    private void PathEffect()
    {
        var position = _casterTransform.position + _casterTransform.forward * 0.5f + new Vector3(0, 1f, 0);
        CastEffect(_fireSpell.PathEffect, 1.7f, position, _casterTransform.rotation);
    }

    private void HitEffect(IDamagable damagable)
    {
        CastEffect(_fireSpell.HitEffect, 1f, damagable.GetPosition(), Quaternion.identity);
    }

    private void CastEffect(GameObject effect, float duration, Vector3 position, Quaternion rotation)
    {
        var pathEffect = UnityEngine.Object.Instantiate(effect, position, rotation);
        UnityEngine.Object.Destroy(pathEffect, duration);
    }

    
}
