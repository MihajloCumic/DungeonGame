using System;
using System.Threading.Tasks;
using UnityEngine;

public class BurnAroundCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly BurnAroundSpell _burnSpell;
    private readonly Func<float> _animationFunc;
    private readonly int playerLayer = LayerMask.NameToLayer("PlayerLayer");

    public BurnAroundCommand(Transform casterTransform, BurnAroundSpell burnSpell, Func<float> animationFunc)
    {
        _casterTransform = casterTransform;
        _burnSpell = burnSpell;
        _animationFunc = animationFunc;
    }
    public async Task Execute()
    {
        _animationFunc();
        BurnAroundIndicatorEffect(_burnSpell.Delay);
        await Awaitable.WaitForSecondsAsync(_burnSpell.Delay);
        LayerMask layerMask = 1 << playerLayer;
        Collider[] hits = Physics.OverlapSphere(_casterTransform.position, _burnSpell.Radius, layerMask);
        foreach (Collider hit in hits)
        {
            if (hit.transform.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(_burnSpell.BaseDamage);
                BurnAroundEffect(damagable);
            }
        }
    }

    private void BurnAroundIndicatorEffect(float duration)
    {
        var casterPostion = _casterTransform.position;
        var center = new Vector3(casterPostion.x, 0f, casterPostion.z);
        var effect = UnityEngine.Object.Instantiate(_burnSpell.IndicatorEffect, center, Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, duration);
    }
    private void BurnAroundEffect(IDamagable damagable)
    {
        var effect = UnityEngine.Object.Instantiate(_burnSpell.HitEffect, damagable.GetPosition(), Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, 1f);
    }
}
