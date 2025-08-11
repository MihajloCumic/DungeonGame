using System;
using System.Threading.Tasks;
using UnityEngine;

public class IceShardsCommand : ICommand
{
    private readonly Transform _casterTransform;
    private readonly IceSpell _iceSpell;
    private readonly Func<float> _animationFunc;
    private readonly Vector3 _center;
    private readonly int enemyLayer = LayerMask.NameToLayer("EnemyLayer");

    public IceShardsCommand(Transform casterTransform, IceSpell iceSpell, Func<float> animationFunc, Vector3 center)
    {
        _casterTransform = casterTransform;
        _iceSpell = iceSpell;
        _animationFunc = animationFunc;
        _center = center;
    }
    public async Task Execute()
    {
        var casterPosition = _casterTransform.position;
        if (Vector3.Distance(casterPosition, _center) > _iceSpell.MaxDistance)
        {
            return;
        }
        float duration = _animationFunc();
        await Awaitable.WaitForSecondsAsync(duration);
        FallingEffect();
        LayerMask layerMask = 1 << enemyLayer;
        for (int i = 0; i < _iceSpell.NumOfCycles; i++)
        {
            Collider[] hits = Physics.OverlapSphere(_center, _iceSpell.SpellRadius, layerMask);
            foreach (Collider hit in hits)
            {
                if (hit.transform.TryGetComponent(out IDamagable damagable))
                {
                    damagable.TakeDamage(_iceSpell.BaseDamage);
                    HitEffect(damagable);
                }
            }
            await Awaitable.WaitForSecondsAsync(_iceSpell.WaitBetweenCycles);
        }

    }



    private void FallingEffect()
    {
        var duration = _iceSpell.NumOfCycles * _iceSpell.WaitBetweenCycles;
        var center = new Vector3(_center.x, 0f, _center.z);
        var effect = UnityEngine.Object.Instantiate(_iceSpell.Indicator);
        effect.transform.position = new Vector3(center.x, 0.1f, center.z);
        var scale = effect.transform.localScale;
        var diameter = _iceSpell.SpellRadius * 2f;
        effect.transform.localScale = new Vector3(diameter, diameter, scale.z);
        UnityEngine.Object.Destroy(effect, duration);
    }
    private void HitEffect(IDamagable damagable)
    {
        var effect = UnityEngine.Object.Instantiate(_iceSpell.HitEffect, damagable.GetPosition(), Quaternion.identity);
        UnityEngine.Object.Destroy(effect.gameObject, 1f);
    }
}
